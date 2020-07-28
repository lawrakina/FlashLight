﻿using System;
using FpsUnity.Controller;
using FpsUnity.Enums;
using FpsUnity.Helper;
using FpsUnity.Interface;
using FpsUnity.Services;
using UnityEngine;
using UnityEngine.AI;

namespace FpsUnity.Model
{
    public class Bot : BaseObjectScene, ISelectObject, IExecute
    {
        #region Fields

        private float Hp = 30;

        #endregion


        #region Properties

        public Vision Vision;
        public Weapon Weapon; //todo с разным оружием 
        public Transform Target { get; set; }
        public NavMeshAgent Agent { get; private set; }
        private float _waitTime = 3;
        private StateBot _stateBot;
        private Vector3 _point;
        private float _stoppingDistance = 10.0f;
        private float _patrolStoppingDistance = 0.0f;

        public event Action<Bot> OnDieChange;
        private ITimeRemaining _timeRemaining;

        private StateBot StateBot
        {
            get => _stateBot;
            set
            {
                _stateBot = value;
                switch (value)
                {
                    case StateBot.None:
                        Color = Color.white;
                        break;
                    case StateBot.Patrol:
                        Color = Color.green;
                        break;
                    case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
                    case StateBot.Detected:
                        Color = Color.red;
                        break;
                    case StateBot.Died:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }

            }
        }

        #endregion


        protected override void Awake()
        {
            base.Awake();
            Agent = GetComponent<NavMeshAgent>();
            Agent.stoppingDistance = _stoppingDistance;
            _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
            Weapon.enabled = true;
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        public void Execute()
        {
            if (StateBot == StateBot.Died) return;

            if (StateBot != StateBot.Detected)
            {
                if (!Agent.hasPath)
                {
                    if (StateBot != StateBot.Inspection)
                    {
                        if (StateBot != StateBot.Patrol)
                        {
                            StateBot = StateBot.Patrol;
                            _point = ServiceLocator.Resolve<EnemyController>().GenericPoint(transform);
                            MovePoint(_point);
                            Agent.stoppingDistance = _patrolStoppingDistance;
                        }
                        else
                        {
                            if ((_point - transform.position).sqrMagnitude <= 1)
                            {
                                StateBot = StateBot.Inspection;
                                _timeRemaining.AddTimeRemaining();
                            }
                        }
                    }
                }

                if (Vision.VisionM(transform, Target))
                {
                    StateBot = StateBot.Detected;
                }
            }
            else
            {
                if (Math.Abs(Agent.stoppingDistance - _stoppingDistance) > Mathf.Epsilon)
                {
                    Debug.Log($"Agent.stoppingDistance {Agent.stoppingDistance}");
                    Agent.stoppingDistance = _stoppingDistance;
                }

                if (Vision.VisionM(transform, Target))
                {
                    Debug.Log($"Weapon.Fire(); {transform.name}, {Target.name}");
                    Weapon.Fire();
                }
                else
                {
                    MovePoint(Target.position);
                }

                //todo Потеря персонажа
            }
        }

        private void ResetStateBot()
        {
            StateBot = StateBot.None;
        }

        private void SetDamage(InfoCollision info)
        {
            //todo реакциия на попадание  
            if (Hp > 0)
            {
                Hp -= info.Damage;
            }

            if (Hp <= 0)
            {
                StateBot = StateBot.Died;
                Agent.enabled = false;
                foreach (var child in GetComponentsInChildren<Transform>())
                {
                    child.parent = null;

                    var tempRbChild = child.GetComponent<Rigidbody>();
                    if (!tempRbChild)
                    {
                        tempRbChild = child.gameObject.AddComponent<Rigidbody>();
                    }
                    //tempRbChild.AddForce(info.Dir * Random.Range(10, 300));

                    Destroy(child.gameObject, 10);
                }

                OnDieChange?.Invoke(this);
            }
        }

        public void MovePoint(Vector3 point)
        {
            Agent.SetDestination(point);
        }


        #region ISelectObject

        public string GetMessage()
        {
            return $"{name}, Hp:{Hp}";
        }

        #endregion
    }
}
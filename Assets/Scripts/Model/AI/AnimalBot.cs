using System;
using System.Collections;
using System.Collections.Generic;
using FpsUnity.Controller;
using FpsUnity.Enums;
using FpsUnity.Interface;
using FpsUnity.Model;
using UnityEngine;
using UnityEngine.AI;


public class AnimalBot : BaseObjectScene, ISelectObject, IExecute
{
    #region Fields

    [SerializeField] private Transform _target;
    private StateBot _state;
    private Animator _animator;
    private NavMeshAgent _navAgent;
    private float _waitTime = 5.0f;
    private TimeRemaining _timeRemaining;

    #endregion

    
    #region Properties

    public StateBot State
    {
        get => _state;
        set
        {
            _state = value;
            switch (_state)
            {
                case StateBot.None:
                    Color = Color.white;
                    _animator.SetFloat(TagManager.SPEED,0f);
                    break;
                case StateBot.Patrol:
                    Color = Color.green;
                    _animator.SetFloat(TagManager.SPEED,1.0f);
                    break;
                case StateBot.Inspection:
                    Color = Color.yellow;
                    break;
                case StateBot.Detected:
                    Color = Color.red;
                    break;
                case StateBot.Attack:
                    Color = Color.magenta;
                    _animator.SetTrigger(TagManager.ATTACK);
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

    #region UnityMethods

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponentInChildren<Animator>();
        _navAgent = GetComponentInChildren<NavMeshAgent>();
        _timeRemaining = new TimeRemaining(ResetStateBot, _waitTime);
        
    }

    private void Start()
    {
        _navAgent.SetDestination(_target.position);
    }

    private void Update()
    {
        _navAgent.SetDestination(_target.position);
        State = StateBot.Patrol;
    }

    #endregion


    private void ResetStateBot()
    {
        State = StateBot.None;
    }
    
    public string GetMessage()
    {
        return $"{Name}";
        // throw new NotImplementedException();
    }

    public void Execute()
    {
        // throw new NotImplementedException();
    }
}

﻿using System;
using FpsUnity.Enums;
using FpsUnity.Interface;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class Aim : MonoBehaviour, ICollision, ISelectObject
    {
        #region Properties

        public event Action OnPointChange = delegate { };

        #endregion


        #region Fields

        public float Hp = 30;
        private bool _isDead;
        private float _timeToDestroy = 10.0f;

        #endregion

        //todo дописать поглощение урона

        #region ICollision

        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (Hp > 0)
            {
                Hp -= info.Damage;

                switch (info.Effect)
                {
                    case EffectType.Fire:
                        gameObject.transform.GetComponent<Renderer>().material.color = Color.red;
                        break;
                }
            }

            if (Hp <= 0)
            {
                if (!TryGetComponent<Rigidbody>(out _))
                {
                    gameObject.AddComponent<Rigidbody>();
                }

                Destroy(gameObject, _timeToDestroy);

                OnPointChange.Invoke();
                _isDead = true;
            }
        }

        #endregion


        #region ISelectObject

        public string GetMessage()
        {
            return $"{gameObject.name}, Hp: {Hp}";
        }

        #endregion
    }
}
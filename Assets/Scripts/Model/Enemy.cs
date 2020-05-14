using System;
using FpsUnity.Controller;
using FpsUnity.Interface;
using FpsUnity.Services;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class Enemy : MonoBehaviour, ISelectObject, ICollision
    {
        #region Properties

        public event Action OnPointChange = delegate { };

        #endregion


        #region Fields

        public float Hp = 30;
        private bool _isDead;
        private float _timeToDestroy = 10.0f;

        #endregion

        //todo дописать поглощение урона ПОДЖЕГ

        #region ICollision

        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;

            //switch (info.InfoCollisionType)
            //{
            //    case InfoCollisionType.Bullet:
                    GettingMomentDamage(info);
            //        break;
            //    case InfoCollisionType.FireBolt:
            //        GettingPeriodicDamage(info);

            //        break;
            //    case InfoCollisionType.FrostBolt:

            //        break;
            //    default:
            //        GettingMomentDamage(info);
            //        break;
            //}

            
        }

        //private void GettingPeriodicDamage(in InfoCollision info)
        //{
        //    _timeRemaining = new TimeRemaining(Burning, _timeBurning, true);
        //}

        //private void Burning()
        //{
        //    var fireEffect = ServiceLocator.Resolve<EffectController>().GetFireEffect();
        //    if (fireEffect)
        //    {
        //        fireEffect.transform.SetParent(transform);
        //    }
        //}

        private void GettingMomentDamage(InfoCollision info)
        {
            if (Hp > 0)
            {
                Hp -= info.Damage;
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
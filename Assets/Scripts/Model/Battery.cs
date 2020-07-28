using FpsUnity.Interface;
using UnityEngine;
using System;
using FpsUnity.Controller;
using FpsUnity.Enums;
using FpsUnity.Services;


namespace FpsUnity.Model
{
    public sealed class Battery : MonoBehaviour, ICollision, ISelectObject
    {
        #region Properties

        public event Action OnPointChange = delegate {};

        #endregion


        #region Fields

        [SerializeField] private float _lightPoints = 10.0f;
        private bool _isDead;

        #endregion


        #region ICollision

        private void OnTriggerEnter(Collider other)
        {
            if(other.name !=  TagManager.PLAYER)
                return;
            Debug.Log(other.name);
            ServiceLocator.Resolve<FlashLightController>().ChangeBattery(_lightPoints);
            Destroy(gameObject, 0.5f);
        }

        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;

            if (!TryGetComponent<Rigidbody>(out _))
            {
                gameObject.AddComponent<Rigidbody>();
            }

            

            OnPointChange.Invoke();
            _isDead = true;
        }

        #endregion


        #region ISelectObject

        public string GetMessage()
        {
            return $"{gameObject.name}, Light time: {_lightPoints}";
        }

        #endregion
    }
}
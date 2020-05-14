using FpsUnity.Controller;
using FpsUnity.Enums;
using FpsUnity.Helper;
using FpsUnity.Interface;
using FpsUnity.Services;
using UnityEngine;


namespace FpsUnity.Model
{
    public abstract class Ammunition : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _timeToDestruct = 4;
        [SerializeField] private float _baseDamage = 10;
        protected float _curDamage; 
        private float _lossOfDamageAtTime = 0.2f;
        private ITimeRemaining _timeRemaining;
        private ITimeRemaining _timePutToPool;

        #endregion


        #region Properties

        public AmmunitionType Type;

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _curDamage = _baseDamage;
        }

        protected virtual void Start()
        {
            //Destroy(gameObject, _timeToDestruct);
            _timeRemaining = new TimeRemaining(LossOfDamage, 1.0f, true);
            _timeRemaining.AddTimeRemaining();

            _timePutToPool = new TimeRemaining(DestroyAmmunition, _timeToDestruct);
            _timePutToPool.AddTimeRemaining();
        }

        #endregion


        #region Methods

        public void AddForce(Vector3 direction)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(direction);
        }

        private void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        protected void DestroyAmmunition()
        {
            _timeRemaining.RemoveTimeRemaining();
            _timePutToPool.RemoveTimeRemaining();
            ServiceLocator.Resolve<PoolController>().PutToPool(this);
        }

        #endregion
    }
}
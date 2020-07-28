using FpsUnity.Controller;
using FpsUnity.Helper;
using FpsUnity.Services;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class Gun : Weapon
    {
        [SerializeField] private readonly float _distanceAttack = 10.0f;
        private Gun()
        {
            // WeaponType = Enums.WeaponType.Ak47;
            _recommendedDistance = _distanceAttack;
        }

        #region Methods

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;

            var tempAmmunition = ServiceLocator.Resolve<PoolController>().GetFromPool(Ammunition) as Ammunition;
            tempAmmunition.transform.position = _barrel.position;
            tempAmmunition.transform.rotation = _barrel.rotation;
            tempAmmunition.AddForce(_barrel.forward * _force);
            // var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);//todo Pool object
            // temAmmunition.AddForce(_barrel.forward * _force);

            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }

        #endregion
    }
}
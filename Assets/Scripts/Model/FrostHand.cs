﻿using FpsUnity.Controller;
using FpsUnity.Helper;
using FpsUnity.Services;


namespace FpsUnity.Model
{
    public sealed class FrostHand : Weapon
    {
        public FrostHand()
        {
            WeaponType = Enums.WeaponType.FrostHand;
        }


        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;

            var tempAmmunition = ServiceLocator.Resolve<PoolController>().GetFromPool(Ammunition) as Ammunition;
            tempAmmunition.transform.position = _barrel.position;
            tempAmmunition.transform.rotation = _barrel.rotation;
            tempAmmunition.AddForce(_barrel.forward * _force);

            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}
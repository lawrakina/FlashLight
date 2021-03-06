﻿using System;
using FpsUnity.Model;
using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class WeaponController : BaseController
    {
        #region Fields

        private Weapon _weapon;

        #endregion


        #region Methods

        public override void On(params BaseObjectScene[] weapon)
        {
            if (IsActive) return;
            Debug.Log($"WeaponController.On, weapon: {weapon}");
            try
            {
                if (weapon.Length > 0)
                {
                    _weapon = weapon[0] as Weapon;
                }
                base.On(_weapon);
                if (_weapon == null)
                    return;
                _weapon.IsVisible = true;
                UiInterface.WeaponUiText.SetActive(true);
                UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Warning! WeaponController.On weapon == null");
            }
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;
            UiInterface.WeaponUiText.SetActive(false);
        }

        public void Fire()
        {
            _weapon.Fire();
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }

        #endregion

        public void ReloadClip()
        {
            _weapon.ReloadClip();
            UiInterface.WeaponUiText.ShowData(_weapon.Clip.CountAmmunition, _weapon.CountClip);
        }
    }
}
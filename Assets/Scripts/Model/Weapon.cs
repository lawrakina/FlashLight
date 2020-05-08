﻿using System.Collections.Generic;
using FpsUnity.Controller;
using FpsUnity.Enums;
using FpsUnity.Interface;
using UnityEngine;


namespace FpsUnity.Model
{
    public abstract class Weapon : BaseObjectScene, IComparer<Weapon>
    {
        #region Fields

        private int _maxCountAmmunition = 40;
        private int _minCountAmmunition = 20;
        private int _countClip = 5;
        public Ammunition Ammunition;
        public Clip Clip;

        [SerializeField] protected Transform _barrel;
        [SerializeField] protected float _force = 999.0f;
        [SerializeField] protected float _rechergeTime = 0.2f;

        private Queue<Clip> _clips = new Queue<Clip>();

        protected bool _isReady = true;
        protected ITimeRemaining _timeRemaining;

        #endregion


        #region Properties

        public WeaponType WeaponType { get; set; }

        public AmmunitionType[] AmmunitionTypes = { AmmunitionType.Bullet };

        public int CountClip => _clips.Count;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechergeTime);
            for (var i = 0; i <= _countClip; i++)
            {
                AddClip(new Clip { CountAmmunition = Random.Range(_minCountAmmunition, _maxCountAmmunition) });
            }

            ReloadClip();
        }

        #endregion


        #region Methods

        protected void ReloadClip()
        {
            if (CountClip <= 0) return;
            Clip = _clips.Dequeue();
        }

        protected void AddClip(Clip clip)
        {
            _clips.Enqueue(clip);
        }

        public abstract void Fire();

        protected void ReadyShoot()
        {
            _isReady = true;
        }

        #endregion


        #region IComparer

        public bool Compare(Weapon other)
        {
            return this.WeaponType == other.WeaponType;
        }

        public int Compare(Weapon x, Weapon y)
        {
            if (x.WeaponType > y.WeaponType)
                return 1;
            if (x.WeaponType < y.WeaponType)
                return -1;
            return 0;
        }

        #endregion
    }

}
using FpsUnity.Enums;
using UnityEngine;


namespace FpsUnity.Model
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Vector3 _direction;
        private readonly float _damage;
        private readonly EffectType _effect;

        #endregion


        #region Properties

        public Vector3 Direction => _direction;

        public float Damage => _damage;

        public EffectType Effect => _effect;

        #endregion


        #region Methods

        public InfoCollision(float damage, Vector3 direction = default, EffectType effect = EffectType.Def)
        {
            _damage = damage;
            _direction = direction;
            _effect = effect;
        }

        #endregion
    }
}
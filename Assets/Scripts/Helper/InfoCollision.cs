using UnityEngine;


namespace FpsUnity.Model
{
    public readonly struct InfoCollision
    {
        #region Fields

        private readonly Vector3 _direction;
        private readonly float _damage;
        private readonly InfoCollisionType _infoCollisionType;

        #endregion


        #region Properties

        public Vector3 Direction => _direction;

        public float Damage => _damage;

        public InfoCollisionType InfoCollisionType => _infoCollisionType;

        #endregion


        #region Methods

        public InfoCollision(InfoCollisionType infoCollisionType ,float damage, Vector3 direction = default)
        {
            _damage = damage;
            _direction = direction;
            _infoCollisionType = infoCollisionType;
        }

        #endregion
    }
}
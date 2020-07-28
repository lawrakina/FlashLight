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
        private readonly ContactPoint _contact;
        private readonly Transform _objCollision;

        #endregion


        #region Properties

        public Vector3 Direction => _direction;

        public float Damage => _damage;

        public EffectType Effect => _effect;
        
        public ContactPoint Contact => _contact;

        public Transform ObjCollision => _objCollision;

        #endregion


        #region Methods

        public InfoCollision(float damage, Vector3 direction, EffectType effect = EffectType.Def)
        {
            _damage = damage;
            _direction = direction;
            _effect = effect;
            _contact = new ContactPoint();
            _objCollision = new RectTransform();
        }
        
        public InfoCollision(float damage, Vector3 direction, ContactPoint contact, Transform objCollision, EffectType effect = EffectType.Def)
        {
            _damage = damage;
            _direction = direction;
            _effect = effect;
            _contact = contact;
            _objCollision = objCollision;
        }

        #endregion
    }
}
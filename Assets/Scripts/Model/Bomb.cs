using FpsUnity.Enums;
using FpsUnity.Interface;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class Bomb : Ammunition
    {
        #region Fields

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity, EffectType.Fire));
            }

        }

        #endregion
    }
}
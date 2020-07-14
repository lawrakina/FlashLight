using FpsUnity.Interface;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class Bullet : Ammunition
    {
        #region Fields

        #endregion


        #region UnityMethods

        private void OnCollisionEnter(Collision collision)//todo своя обработка полета и получения урона
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }

        #endregion
    }
}
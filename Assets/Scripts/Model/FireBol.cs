using FpsUnity.Interface;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class FireBol : Ammunition
    {
        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }
    }
}
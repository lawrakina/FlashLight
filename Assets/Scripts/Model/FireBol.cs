using FpsUnity.Interface;
using UnityEngine;

namespace FpsUnity.Model
{
    public sealed class FireBol : Ammunition
    {
        private InfoCollisionType collisionType = InfoCollisionType.FireBolt;

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(collisionType, _curDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }
    }
}
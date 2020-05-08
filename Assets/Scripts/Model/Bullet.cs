using FpsUnity.Interface;
using UnityEngine;


namespace FpsUnity.Model
{                  
    public sealed class Bullet : Ammunition
    {
        private void OnCollisionEnter(Collision collision)//todo своя обработка полета и получения урона
        {
            //дописать свой урон

            var setDamage = collision.gameObject.GetComponent<ICollision>();

            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_curDamage, Rigidbody.velocity));
            }

            DestroyAmmunition();
        }
    }
}
using System;
using FpsUnity.Interface;
using FpsUnity.Model;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class HeadBot : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChange;
        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(new InfoCollision(info.Damage * 500, info.Direction, info.Contact, info.ObjCollision));
        }

        public void CollisionEnter(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(new InfoCollision(info.Damage * 500, info.Direction, info.Contact, info.ObjCollision));
        }
    }
}
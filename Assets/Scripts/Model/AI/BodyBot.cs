using System;
using FpsUnity.Interface;
using FpsUnity.Model;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class BodyBot : MonoBehaviour, ICollision
    {
        public event Action<InfoCollision> OnApplyDamageChange;

        public void OnCollision(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(info);
        }

        public void CollisionEnter(InfoCollision info)
        {
            OnApplyDamageChange?.Invoke(info);
        }
    }
}
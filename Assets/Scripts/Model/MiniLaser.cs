using FpsUnity.Controller;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class MiniLaser : Weapon
    {
        private Camera _mainCamera;
        private Vector2 _center;
        
        private Camera MainCamera{
            get { 
                if (!_mainCamera) 
                    _mainCamera = Camera.main;
                return _mainCamera;
            }
            
        }
        
        private MiniLaser()
        {
            WeaponType = Enums.WeaponType.MiniLaser;
            _mainCamera = Camera.main;
            _center = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;

            if (Physics.Raycast(MainCamera.ScreenToWorldPoint(_center), _barrel.forward, out var raycastHit, 20.0f))
            {
                Debug.DrawRay(MainCamera.ScreenToWorldPoint(_center), _barrel.forward * raycastHit.distance, Color.yellow);
                Debug.Log(raycastHit.collider.gameObject);
                raycastHit.collider.gameObject.transform.localScale *= 0.9f;
            }
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}
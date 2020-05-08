using FpsUnity.Enums;
using FpsUnity.Interface;
using FpsUnity.Model;
using FpsUnity.Services;
using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;
        private KeyCode _cansel = KeyCode.Escape;
        private KeyCode _reloadClip = KeyCode.R;
        private KeyCode _selectWeapon1 = KeyCode.Alpha1;   
        private int _mouseButton = (int) MouseButton.LeftButton;

        #endregion


        #region Mehtods

        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }   

        private void SelectWeapon(int i)
        {
            ServiceLocator.Resolve<WeaponController>().Off();
            var tempWeapon = ServiceLocator.Resolve<Inventory>().GetWeaponByIndex(i);
            //var tempWeapon = ServiceLocator.Resolve<Inventory>().Weapons[i];//todo инкапсулировать
            if (tempWeapon != null)
            {
                ServiceLocator.Resolve<WeaponController>().On(tempWeapon);
            }
        }

        #endregion


        #region UnityMethods

        public void Execute()
        {
            //Debug.Log($"InputController.Execute - IsActive: {IsActive}, Input.GetKeyDown({Input.GetKeyDown(_activeFlashLight)})");
            if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().
                    Switch(ServiceLocator.Resolve<Inventory>().FlashLight);
            }

            //todo реализовать выбор оружия по колесу мыши

            if (Input.GetKeyDown(_selectWeapon1))
            {
                SelectWeapon(0);
            }

            if (Input.GetMouseButton(_mouseButton))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().Fire();
                }
            }

            if (Input.GetKeyDown(_cansel))
            {
                ServiceLocator.Resolve<WeaponController>().Off();
                ServiceLocator.Resolve<FlashLightController>().Off();
            }

            if (Input.GetKeyDown(_reloadClip))
            {
                if (ServiceLocator.Resolve<WeaponController>().IsActive)
                {
                    ServiceLocator.Resolve<WeaponController>().ReloadClip();
                }
            }
        }

        #endregion
    }
}
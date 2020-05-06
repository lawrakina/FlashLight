using FpsUnity.Interface;
using FpsUnity.Services;
using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        #region Fields

        private KeyCode _activeFlashLight = KeyCode.F;

        #endregion


        #region UnityMethods

        public void Execute()
        {
            //Debug.Log($"InputController.Execute - IsActive: {IsActive}, Input.GetKeyDown({Input.GetKeyDown(_activeFlashLight)})");
            //if (!IsActive) return;
            if (Input.GetKeyDown(_activeFlashLight))
            {
                ServiceLocator.Resolve<FlashLightController>().Switch();
            }
        }

        #endregion
    }
}
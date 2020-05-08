using FpsUnity.Interface;
using UnityEngine;
using FpsUnity.Services;


namespace FpsUnity.Controller
{
    public sealed class Controllers : IInitualization
    {
        #region Properties

        public int Lenght => _executeControllers.Length;

        public IExecute this[int index] => _executeControllers[index];

        #endregion


        #region Fields

        private readonly IExecute[] _executeControllers;

        #endregion


        #region  UnityMethods   

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitualization initualization)
                {
                    initualization.Initialization();
                }
            }

            ServiceLocator.Resolve<PlayerController>().On();
            ServiceLocator.Resolve<FlashLightController>().On();
            ServiceLocator.Resolve<InputController>().On();
        }

        #endregion


        #region Methods

        public Controllers()
        {
            //if (Application.platform == RuntimePlatform.PS4)
            //{

            //    ///
            //}
            //else
            //{

            //}

            
            IMotor motor = default;
            motor = new UnitMotor(ServiceLocatorMonoBehavior.GetService<CharacterController>());
            ServiceLocator.SetService(new PlayerController(motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new InputController());
            _executeControllers = new IExecute[3];

            _executeControllers[0] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[1] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[2] = ServiceLocator.Resolve<InputController>();
        }

        #endregion
    }
}
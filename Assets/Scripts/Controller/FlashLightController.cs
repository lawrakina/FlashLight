using FpsUnity.Interface;
using FpsUnity.Model;
using FpsUnity.View;
using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class FlashLightController : BaseController, IExecute, IInitualization
    {
        #region Fields

        private FlashLightModel _flashLightModel;
        private FlashLightUi _flashLightUi;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUi = Object.FindObjectOfType<FlashLightUi>();

            //Debug.Log($"_flashLightModel: {_flashLightModel}, _flashLightUi: {_flashLightUi}");

        }

        public void Execute()
        {
            _flashLightUi.Text = _flashLightModel.BatteryChargeCurrent;
            _flashLightUi.Color = _flashLightModel.GetColorBattery;

            if (!IsActive)
            {
                _flashLightModel.ChargeBattery();
                return;
            }

            if (!_flashLightModel.BatteryUsage())
            {
                _flashLightModel.ChargeBattery();
                Off();
            }


            _flashLightModel.Rotation();
        }

        #endregion


        #region Methods

        public override void On()
        {
            if (IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);
            //_flashLightUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            //_flashLightUi.SetActive(false);
        }

        #endregion

    }
}
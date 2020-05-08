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
        private FlashLightUiText _flashLightUiText;

        #endregion


        #region UnityMethods

        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();
            _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();

            //Debug.Log($"_flashLightModel: {_flashLightModel}, _flashLightUiText: {_flashLightUiText}");

        }

        public void Execute()
        {
            _flashLightUiText.Text = _flashLightModel.BatteryChargeCurrent;
            _flashLightUiText.Color = _flashLightModel.GetColorBattery;

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
            //_flashLightUiText.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);
            //_flashLightUiText.SetActive(false);
        }

        #endregion

    }
}
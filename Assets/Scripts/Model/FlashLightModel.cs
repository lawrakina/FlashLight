using System;
using UnityEngine;


namespace FpsUnity.Model
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        #region Fields

        [SerializeField] private float _speedRotation = 11f;
        [SerializeField] private float _batteryChargeMax = 10f;
        [SerializeField] private float _persentLowBatteryCharge = 20f;
        [SerializeField] private float _persentFullBatteryCharge = 80f;

        private Light _light;
        private Transform _goFollow;
        private Vector3 _vectorOffset;
        private readonly Color _lowChargeColor = Color.red;
        private readonly Color _fullChargeColor = Color.green;
        private readonly Color _mediumChargeColor = Color.white;
        private readonly Color _chargingBatteryColor = Color.blue;
        private Color _currentBatteryColor;

        #endregion

        #region Properties

        public float BatteryChargeCurrent { get; private set; }

        public Color BatteryGetColor
        {
            get
            {
                if (!_light.enabled)
                {
                    _currentBatteryColor = _chargingBatteryColor;
                }
                else
                if (BatteryChargeCurrent >= (_persentFullBatteryCharge * 0.01 * _batteryChargeMax))
                {
                    _currentBatteryColor = _fullChargeColor;
                }
                else if (BatteryChargeCurrent <= (_persentLowBatteryCharge * 0.01 * _batteryChargeMax))
                {
                    _currentBatteryColor = _lowChargeColor;
                }
                else
                {
                    _currentBatteryColor = _mediumChargeColor;
                }
                return _currentBatteryColor;
            }
        }

        #endregion


        #region UnityMethods

        protected override void Awake()
        {
            base.Awake();
            _light = GetComponent<Light>();
            _goFollow = Camera.main.transform;
            _vectorOffset = Transform.position - _goFollow.position;
            BatteryChargeCurrent = _batteryChargeMax;
        }

        #endregion


        #region Methods

        public void Switch(FlashLightActiveType value)
        {
            //Debug.Log($"FlashLightModel.Switch({value})");
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vectorOffset;
                    Transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vectorOffset;
            Transform.rotation = Quaternion.Lerp(
                Transform.rotation,
                _goFollow.rotation,
                _speedRotation * Time.deltaTime);
        }

        public bool BatteryUsage()
        {
            var result = false;
            if (BatteryChargeCurrent > 0)
            {
                BatteryChargeCurrent -= Time.deltaTime;
                result = true;
            }
            return result;
        }

        public void ChargeBattery()
        {
            if (BatteryChargeCurrent < _batteryChargeMax)
            {
                //Debug.Log($"ChargeBattery.BatteryChargeCurrent: {BatteryChargeCurrent}");
                BatteryChargeCurrent += Time.deltaTime;
            }
        }

        public void ChargeBattery(float delta)
        {
            BatteryChargeCurrent += delta;
            if (BatteryChargeCurrent > _batteryChargeMax)
            {
                BatteryChargeCurrent = _batteryChargeMax;
            }
        }

        #endregion
    }
}
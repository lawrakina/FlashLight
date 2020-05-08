using UnityEngine;


namespace FpsUnity.View
{
    public sealed class UiInterface
    {
        #region Fields

        private FlashLightUi _flashLightUi;
        private FlashLightUiBar _flashLightUiBar;
        private WeaponUiText _weaponUiText;
        private SelectionObjMessageUi _selectionObjMessageUi;

        #endregion


        #region Properties

        public FlashLightUi LightUi
        {
            get
            {
                if (!_flashLightUi)
                    _flashLightUi = Object.FindObjectOfType<FlashLightUi>();
                return _flashLightUi;
            }
        }


        public FlashLightUiBar FlashLightUiBar
        {
            get
            {
                if (!_flashLightUiBar)
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                return _flashLightUiBar;
            }
        }


        public WeaponUiText WeaponUiText
        {
            get
            {
                if (!_weaponUiText)
                    _weaponUiText = Object.FindObjectOfType<WeaponUiText>();
                return _weaponUiText;
            }
        }


        public SelectionObjMessageUi SelectionObjMessageUi
        {
            get
            {
                if (!_selectionObjMessageUi)
                    _selectionObjMessageUi = Object.FindObjectOfType<SelectionObjMessageUi>();
                return _selectionObjMessageUi;
            }
        }

        #endregion
    }
}

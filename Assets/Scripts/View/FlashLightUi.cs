using UnityEngine;
using UnityEngine.UI;


namespace FpsUnity.View
{
    public sealed class FlashLightUi : MonoBehaviour
    {

        #region Properties

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        public Color Color
        {
            set => _text.color = value;
        }      

        public void SetActive(bool value)
        {
            //print($"FlashLightUi.SetActive({value})");
            _text.gameObject.SetActive(value);
        }

        #endregion


        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
            //print($"FlashLightUi.Awake()");
        }

        #endregion
    }
}
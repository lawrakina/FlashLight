using UnityEngine;
using UnityEngine.UI;


namespace FpsUnity.View
{
    public class FlashLightUiText : MonoBehaviour
    {      
        #region Properties

        public float Text
        {
            set => _text.text = $"{value:0.0}";
        }

        #endregion
        
        
        #region Fields

        private Text _text;

        #endregion


        #region UnityMethods

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        #endregion


        #region Methods
        
        public void SetActive(bool value)
        {
            _text.gameObject.SetActive(value);
        } 

        #endregion
    }
}

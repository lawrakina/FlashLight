﻿using UnityEngine;
using UnityEngine.UI;


namespace FpsUnity.View
{
    public sealed class SelectionObjMessageUi : MonoBehaviour
    {
        #region Fields

        private Text _text;

        #endregion


        #region Properties

        public string Text
        {
            set => _text.text = $"{value}";
        }

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
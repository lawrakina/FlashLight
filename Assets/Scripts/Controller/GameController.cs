﻿using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }

        private void Update()
        {
            for (var i = 0; i < _controllers.Lenght; i++)
            {
                _controllers[i].Execute();
                //print($"_controllers[i].GetType: {_controllers[i].GetType()}");
            }
        }

        #endregion
    }
}
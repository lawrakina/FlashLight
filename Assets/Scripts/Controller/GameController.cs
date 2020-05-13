using FpsUnity.Helper;
using FpsUnity.Model;
using UnityEngine;


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


            PoolManager.Init(Object.FindObjectOfType<GameController>().transform);
        }

        private void Update()
        {
            for (var i = 0; i < _controllers.Lenght; i++)
            {
                _controllers[i].Execute();
            }
        }

        #endregion
    }
}
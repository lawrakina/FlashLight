using FpsUnity.Helper;
using FpsUnity.Model;
using UnityEngine;


namespace FpsUnity.Controller
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private int _countBullet1;
        [SerializeField] private Ammunition _bullet1;
        [SerializeField] private int _countBullet2;
        [SerializeField] private Ammunition _bullet2;
        [SerializeField] private int _countBullet3;
        [SerializeField] private Ammunition _bullet3;
        [SerializeField] private int _countBullet4;
        [SerializeField] private Ammunition _bullet4;

        private Controllers _controllers;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();


            PoolManager.Init(Object.FindObjectOfType<GameController>().transform);
            //for (int i = 0; i < _countBullet1; i++)
            //    PoolManager.PutToPool(_bullet1);
            //for (int i = 0; i < _countBullet2; i++)
            //    PoolManager.PutToPool(_bullet2); 
            //for (int i = 0; i < _countBullet3; i++)
            //    PoolManager.PutToPool(_bullet3); 
            //for (int i = 0; i < _countBullet4; i++)
            //    PoolManager.PutToPool(_bullet4);
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
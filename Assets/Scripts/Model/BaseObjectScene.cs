using UnityEngine;


namespace FpsUnity.Model
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Properties

        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;
                AskLayer(Transform, _layer);
            }
        }

        #endregion


        #region Fields

        private int _layer;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }

        #endregion


        #region Methods

        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;
            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        #endregion
    }
}
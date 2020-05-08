using UnityEngine;


namespace FpsUnity.Model
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Properties

        [HideInInspector] public Rigidbody Rigidbody { get; private set; }
        [HideInInspector] public Transform Transform { get; private set; }

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
                AskLayer(transform, value);
            }
        }

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                AskColor(transform, _color);
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RendererSetActive(transform);
                if (transform.childCount <= 0) return;
                foreach (Transform transform1 in transform)
                {
                    RendererSetActive(transform1);
                }
            }
        }

        #endregion


        #region Fields

        private int _layer;
        private Color _color;
        private bool _isVisible;

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform =  GetComponent<Transform>();
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

        private void RendererSetActive(Transform renderer)
        {
            if (renderer.gameObject.TryGetComponent<Renderer>(out var component))
            {
                component.enabled = _isVisible;
            }
        }

        private void AskColor(Transform obj, Color color)
        {
            foreach (var currentMaterial in obj.GetComponent<Renderer>().materials)
            {
                currentMaterial.color = color;
            }
            if (obj.childCount <= 0) return;
            foreach (Transform child in obj)
            {
                AskColor(child, color);
            }
        }

        public void DisableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidbodyChild in rigidbodies)
            {
                rigidbodyChild.isKinematic = true;
            }
        }

        public void EnableRigidBody(float force)
        {
            EnableRigidBody();
            Rigidbody.AddForce(transform.forward * force);
        }

        public void EnableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidbodyChild in rigidbodies)
            {
                rigidbodyChild.isKinematic = false;
            }
        }

        /// <summary>
        /// Замораживает или размораживает физическую трансформацию объекта
        /// </summary>
        /// <param name="rigidbodyConstraints">Трансформацию которую нужно заморозить</param>
        public void ConstraintsRigidBody(RigidbodyConstraints rigidbodyConstraints)
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.constraints = rigidbodyConstraints;
            }
        }

        public void SetActive(bool value)
        {
            IsVisible = value;
            if (TryGetComponent<Collider>(out var component))
            {
                component.enabled = value;
            }
        }

        #endregion
    }
}
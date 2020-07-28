﻿using UnityEngine;


namespace FpsUnity.Model
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        private int _layer;
        private Color _color;
        private bool _isVisible;
        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public Transform Transform;

        #region UnityFunction

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = GetComponent<Transform>();
        }

        #endregion

        #region Property

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }

        /// <summary>
        /// Слой объекта
        /// </summary>
        public int Layer
        {
            get => _layer;

            set
            {
                _layer = value;
                AskLayer(transform, value);
            }
        }

        /// <summary>
        /// Цвет материала объекта
        /// </summary>
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
                foreach (Transform t in transform)
                {
                    RendererSetActive(t);
                }
            }
        }

        #endregion

        #region PrivateFunction

        /// <summary>
        /// Выставляет слой себе и всем вложенным объектам в независимости от уровня вложенности
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="lvl">Слой</param>
        private void AskLayer(Transform obj, int lvl)
        {
            obj.gameObject.layer = lvl;
            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskLayer(d, lvl);
            }
        }

        private void RendererSetActive(Transform renderer)
        {
            var component = renderer.gameObject.GetComponent<Renderer>();
            if (component)
                component.enabled = _isVisible;
        }

        private void AskColor(Transform obj, Color color)
        {
            if (obj.TryGetComponent<Renderer>(out var renderer))
            {
                foreach (var curMaterial in renderer.materials)
                {
                    curMaterial.color = color;
                }
            }

            if (obj.childCount <= 0) return;
            foreach (Transform d in obj)
            {
                AskColor(d, color);
            }
        }

        #endregion

        /// <summary>
        /// Выключает физику у объекта и его детей
        /// </summary>
        public void DisableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }

        /// <summary>
        /// Включает физику у объекта и его детей
        /// </summary>
        public void EnableRigidBody(float force)
        {
            EnableRigidBody();
            Rigidbody.AddForce(transform.forward * force);
        }

        /// <summary>
        /// Включает физику у объекта и его детей
        /// </summary>
        public void EnableRigidBody()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (var rb in rigidbodies)
            {
                rb.isKinematic = false;
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
        
        public void SetDefault()
        {
            Transform.position = Vector3.zero;
            Transform.rotation = Quaternion.identity;
            Rigidbody.velocity = Vector3.zero;

            var child = GetComponentInChildren<TrailRenderer>();
            if(child)
                child.Clear();
        }

        public void SetActive(bool value)
        {
            IsVisible = value;

            var tempCollider = GetComponent<Collider>();
            if (tempCollider)
            {
                tempCollider.enabled = value;
            }
        }
    }
}
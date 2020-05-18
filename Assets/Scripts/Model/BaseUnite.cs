using System;
using FpsUnity.Interface;
using FpsUnity.Model;
using UnityEngine;

public abstract class BaseUnite : BaseObjectScene, ICollision, ISelectObject
{
    #region Properties

    public event Action OnPointChange = delegate { };

    public EffectVisual EffectVisual
    {
        get { return _effectVisual; }
        set { _effectVisual = value; }
    }
    #endregion


    #region Fields

    public float Hp = 30;
    private bool _isDead;
    private float _timeToDestroy = 10.0f;
    private EffectVisual _effectVisual;

    #endregion

    //todo дописать поглощение урона

    #region ICollision

    public void CollisionEnter(InfoCollision info)
    {
        if (_isDead) return;
        if (Hp > 0)
        {
            Hp -= info.Damage;
        }

        if (Hp <= 0)
        {
            if (!TryGetComponent<Rigidbody>(out _))
            {
                gameObject.AddComponent<Rigidbody>();
            }

            Destroy(gameObject, _timeToDestroy);

            OnPointChange.Invoke();
            _isDead = true;
        }
    }

    #endregion


    #region ISelectObject

    public string GetMessage()
    {
        return $"{gameObject.name}, Hp: {Hp}";
    }

    #endregion
}
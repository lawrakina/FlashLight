using FpsUnity.Controller;
using UnityEngine;


public sealed class EffectController : BaseController
{
    #region Fields

    private EffectsModel _effectsModel;

    #endregion



    #region Properties

    public GameObject GetFireEffect() => _effectsModel.FireEffect;

    #endregion


    #region Methods

    public void Init()
    {
        _effectsModel = Object.FindObjectOfType<EffectsModel>();
    }

    #endregion
}

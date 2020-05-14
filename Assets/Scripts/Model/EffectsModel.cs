using UnityEngine;


public sealed class EffectsModel : MonoBehaviour
{
    #region Properties

    [SerializeField] private GameObject _fireEffect;
    [SerializeField] private GameObject _frostEffect;

    #endregion
    
    
    #region Properties

    public GameObject FireEffect => _fireEffect;

    public GameObject FrostEffect =>_frostEffect;

    #endregion
}
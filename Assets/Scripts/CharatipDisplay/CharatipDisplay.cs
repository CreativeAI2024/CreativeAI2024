using UnityEngine;

abstract public class CharatipDisplay : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer charatip;

    void Start()
    {
        ChangeCharatipVisibility();
        FlagManager.Instance.OnFlagChanged += ChangeCharatipVisibility;
    }
    public abstract void ChangeCharatipVisibility();
}
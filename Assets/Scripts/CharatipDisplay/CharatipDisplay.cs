using UnityEngine;

abstract public class CharatipDisplay : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer charatip;
    void Start()
    {
        DebugLogger.Log($"ChangeCharatipVisibility() added.", DebugLogger.Colors.Yellow);
        FlagManager.Instance.OnFlagChanged += ChangeCharatipVisibility;
    }

    protected abstract void ChangeCharatipVisibility();
}
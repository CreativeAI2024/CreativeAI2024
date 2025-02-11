using UnityEngine;

abstract public class CharatipDisplay : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer charatip;

    public abstract void ChangeCharatipVisibility();
}
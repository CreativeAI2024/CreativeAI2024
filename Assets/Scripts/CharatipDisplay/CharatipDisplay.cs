using UnityEngine;

abstract public class CharatipDisplay : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer charatip;

    protected abstract void Update();
}
using UnityEngine;
using UnityEngine.Tilemaps;
public class DisableHikaruSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hikaruSprite;
    [SerializeField] private TilemapCollider2D tilemapCollider;

    void Start()
    {
        if (FlagManager.Instance.HasFlag("Hikaru_Dead"))
        {
            hikaruSprite.enabled = false;
            tilemapCollider.enabled = false;
        }
    }
}
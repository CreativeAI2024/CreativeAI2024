using UnityEngine;
public class DisableHikaruSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hikaruSprite;

    void Start()
    {
        if (FlagManager.Instance.HasFlag("Hikaru_Dead"))
        {
            hikaruSprite.enabled = false;
        }
    }
}
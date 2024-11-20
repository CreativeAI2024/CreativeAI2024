using UnityEngine;

public class SearchGameCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cursorImage;
    [SerializeField] private Sprite searchingCursor;
    [SerializeField] private Sprite FoundCursor;

    public void SetIsFocusing(bool isFocusing)
    {
        cursorImage.sprite = isFocusing ? FoundCursor : searchingCursor;
    }
}

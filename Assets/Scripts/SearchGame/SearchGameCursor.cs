using UnityEngine;

public class SearchGameCursor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cursorImage;
    [SerializeField] private Sprite arrowCursor;
    [SerializeField] private Sprite handCursor;

    public void SetIsFocusing(bool isFocusing)
    {
        cursorImage.sprite = isFocusing ? handCursor : arrowCursor;
    }
}

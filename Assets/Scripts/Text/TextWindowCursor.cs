using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextWindowCursor : MonoBehaviour
{
    [SerializeField] private GameObject cursorObject;
    [SerializeField] private int cursorOffset;

    public void CursorMove(Vector2 targetCursorPosition)
    {
        Vector2 cursorPosition = new Vector2(targetCursorPosition.x - cursorOffset, targetCursorPosition.y);
        cursorObject.transform.position = cursorPosition;
    }

    public void SetVisibleCursor(bool isVisible)
    {
        cursorObject.SetActive(isVisible);
    }    
}

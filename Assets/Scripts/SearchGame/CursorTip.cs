using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorTip : MonoBehaviour
{
    [SerializeField] private Renderer cursorTip;
    [SerializeField] private SearchGameCursor cursor;
    [SerializeField] private SearchGameManager searchGameManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        cursor.SetIsFocusing(true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        cursor.SetIsFocusing(false);
    }
}

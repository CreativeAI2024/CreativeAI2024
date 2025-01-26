using UnityEngine;

public class MouseHide : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }
}
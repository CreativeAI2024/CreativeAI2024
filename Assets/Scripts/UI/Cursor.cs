using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    void Update()
    {
        SetPosition();
    }
    private void SetPosition()
    {
        if (!IsCurrentEventSystemNull())
        {
            transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }
    }

    private bool IsCurrentEventSystemNull()
    {
        return EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null;
    }
}

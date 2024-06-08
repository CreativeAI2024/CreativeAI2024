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
        if (!UIManager.IsCurrentEventSystemNull())
        {
            transform.position = EventSystem.current.currentSelectedGameObject.transform.position;

        }
    }
}

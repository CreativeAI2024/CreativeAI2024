using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCursor : MonoBehaviour
{
    void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        if (EventSystem.current != null)
        {
            transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }
    }
}

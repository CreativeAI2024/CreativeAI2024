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
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            if (selectedObject != null)
            {
                transform.position = selectedObject.transform.position;
            }
        }
    }
}

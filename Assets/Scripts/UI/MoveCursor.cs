using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCursor : MonoBehaviour
{
    void OnEnable()
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

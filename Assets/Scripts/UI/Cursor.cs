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
        if (!UIUtility.IsCurrentEventSystemNull())
        {
            transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }
    }
}

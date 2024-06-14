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
            Debug.Log("currentSelectedGameObject: "+EventSystem.current.currentSelectedGameObject);
            Debug.Log("this: "+this.gameObject);
            transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetFirstButtonFocus : MonoBehaviour
{
    private bool isOnEnableFirstRun = true;
    void OnEnable()
    {
        if (isOnEnableFirstRun)
        {
            isOnEnableFirstRun = false;
        }
        else
        {
            Focus();
        }
    }
    void Start()
    {
        Focus();
    }
    private void Focus()
    {
        if (transform.childCount > 0) 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject obj = transform.GetChild(0).gameObject;
                if (obj.activeSelf)
                {
                    EventSystem.current.SetSelectedGameObject(obj);
                    break;
                }
            }
        }
        else if (TryGetComponent<Selectable>(out Selectable _))
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}

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
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
            Debug.Log("EventSystem.current.currentSelectedGameObject: "+EventSystem.current.currentSelectedGameObject);
        }
        else if (gameObject.GetComponent<Selectable>())
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
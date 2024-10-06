using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetFirstButtonFocus : MonoBehaviour
{
    void OnEnable()
    {
        Focus();
    }
    void Start()
    {
        Focus();
    }
    private void Focus()
    {
        if (transform.childCount > 0) 
        {
            Debug.Log("transform: "+transform);
            Debug.Log("transform.GetChild(0).gameObject: "+transform.GetChild(0).gameObject);
            Debug.Log("EventSystem.current: "+EventSystem.current);
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }
        else if (gameObject.GetComponent<Selectable>())
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
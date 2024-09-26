using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetFirstButtonFocus : MonoBehaviour
{
    // ItemWindowの初回起動時では、まだItemButtonが生成されていないからOnEnableだとダメだった
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
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }
        else if (gameObject.GetComponent<Selectable>())
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
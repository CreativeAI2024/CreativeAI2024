using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// Windowじゃなく先頭の子オブジェクトにアタッチする
// ItemWindowではスクリプトで自動アタッチする
// ↓
// Buttonsにアタッチする
public class SetFirstButtonFocus : MonoBehaviour
{
    void OnEnable() // ItemWindowでは、まだItemButtonが生成されていないから初回はOnEnableだとダメだった
    {
        // Debug.Log("(OnEnable())SetFirstButtonFocus on "+transform.parent.gameObject);
        Focus();
    }
    void Start()
    {
        // Debug.Log("(Start())SetFirstButtonFocus on "+transform.parent.gameObject);
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
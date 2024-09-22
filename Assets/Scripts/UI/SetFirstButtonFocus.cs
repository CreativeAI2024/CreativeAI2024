using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// Windowじゃなく先頭の子オブジェクトにアタッチする
// ItemWindowではスクリプトで自動アタッチする
// ↓
// Buttonsにアタッチする
public class SetFirstButtonFocus : MonoBehaviour
{
    void OnEnable() //初回はOnEnableだとダメだった
    {
        Debug.Log("SetFirstButtonFocus on "+transform.parent.gameObject);
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
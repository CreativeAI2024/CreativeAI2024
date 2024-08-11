using UnityEngine;
using UnityEngine.EventSystems;
// Windowじゃなく先頭の子オブジェクトにアタッチする
// ItemWindowではスクリプトで自動アタッチする
// ↓
// Buttonsにアタッチする
public class SetFirstButtonFocus : MonoBehaviour
{
    void OnEnable() //初回はOnEnableだとダメだった
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
        EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
    }
}
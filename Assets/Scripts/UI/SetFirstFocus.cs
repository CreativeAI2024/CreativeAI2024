using UnityEngine;
using UnityEngine.EventSystems;
// Windowじゃなく先頭の子オブジェクトにアタッチする
// ItemWindowではスクリプトでアタッチする
public class SetFirstFocus : MonoBehaviour
{
    [SerializeField] private GameObject cursor;
    void OnEnable()
    {
        SetButtonFocus();
        SetCursorPosition();
    }
    private void SetButtonFocus()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void SetCursorPosition()
    {
        if (EventSystem.current!=null)
        cursor.transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}

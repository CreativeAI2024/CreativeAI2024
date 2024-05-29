using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ウィンドウを開く、閉じる機能、コンストラクタで縦横比率等を指定できるようにする
public class BaseWindow : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.CloseWindow();
    }

    public void OpenWindow() 
    {
        ChangeActive(true);
    }

    public void CloseWindow()
    {
        ChangeActive(false);
    }

    private void ChangeActive(bool b) {
        this.gameObject.SetActive(b);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class KeySoundPlayer : MonoBehaviour
{
    //https://unity-shoshinsha.biz/archives/1119#Button-4
    //Button button;

    //キー操作で動かすためのコード
    public Button[] buttons;
    private int currentButtonIndex = 0;

    //矢印キー押したら効果音
    public AudioClip selectSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SelectButton(currentButtonIndex);

        //以下のコードはこれを参考　https://unity-shoshinsha.biz/archives/1119#Button-4
        //button = GameObject.Find("Canvas/Button").GetComponent<Button>();
        //button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 上矢印キーで前のボタンを選択
            currentButtonIndex--;
            if (currentButtonIndex < 0)
                currentButtonIndex = buttons.Length - 1;
            SelectButton(currentButtonIndex);

            //矢印キー押したら効果音
            PlaySelectSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 下矢印キーで次のボタンを選択
            currentButtonIndex++;
            if (currentButtonIndex >= buttons.Length)
                currentButtonIndex = 0;
            SelectButton(currentButtonIndex);
            PlaySelectSound();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Zキーで現在選択されているボタンをクリック
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }

    void SelectButton(int index)
    {
        // ボタンを選択
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }

    void PlaySelectSound()
    {
        if (audioSource != null && selectSound != null)
        {
            audioSource.PlayOneShot(selectSound);
        }
    }
}

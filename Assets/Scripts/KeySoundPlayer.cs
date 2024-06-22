using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class KeySoundPlayer : MonoBehaviour
{
    public Button[] buttons;
    private int currentButtonIndex = 0;

    // 選択音のAudioClip
    public AudioClip selectSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SelectButton(currentButtonIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 現在のボタンインデックスをデクリメント
            currentButtonIndex--;
            if (currentButtonIndex < 0)
                currentButtonIndex = buttons.Length - 1;
            SelectButton(currentButtonIndex);

            // 選択音を再生
            PlaySelectSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 現在のボタンインデックスをインクリメント
            currentButtonIndex++;
            if (currentButtonIndex >= buttons.Length)
                currentButtonIndex = 0;
            SelectButton(currentButtonIndex);
            PlaySelectSound();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Zキーが押された場合、現在のボタンをクリック
            //'Button' does not contain a definition for 'onClick' and no accessible extension method 'onClick' accepting a first argument of type 'Button' could be found (are you missing a using directive or an assembly reference?)CS1061
            //buttons[currentButtonIndex].onClick.Invoke();
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

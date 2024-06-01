using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class KeySoundPlayer : MonoBehaviour
{
    //https://unity-shoshinsha.biz/archives/1119#Button-4
    //Button button;

    //�L�[����œ��������߂̃R�[�h
    public Button[] buttons;
    private int currentButtonIndex = 0;

    //���L�[����������ʉ�
    public AudioClip selectSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        SelectButton(currentButtonIndex);

        //�ȉ��̃R�[�h�͂�����Q�l�@https://unity-shoshinsha.biz/archives/1119#Button-4
        //button = GameObject.Find("Canvas/Button").GetComponent<Button>();
        //button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ����L�[�őO�̃{�^����I��
            currentButtonIndex--;
            if (currentButtonIndex < 0)
                currentButtonIndex = buttons.Length - 1;
            SelectButton(currentButtonIndex);

            //���L�[����������ʉ�
            PlaySelectSound();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // �����L�[�Ŏ��̃{�^����I��
            currentButtonIndex++;
            if (currentButtonIndex >= buttons.Length)
                currentButtonIndex = 0;
            SelectButton(currentButtonIndex);
            PlaySelectSound();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Z�L�[�Ō��ݑI������Ă���{�^�����N���b�N
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }

    void SelectButton(int index)
    {
        // �{�^����I��
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

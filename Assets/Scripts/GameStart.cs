using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    private bool firstPush = false;
    private InputSetting _inputSetting;

    private void Start()
    {
        _inputSetting = InputSetting.Load();
        SoundManager.Instance.PlayBGM(7, 1f);
    }

    private void Update()
    {
        if (_inputSetting.GetDecideInput() && EventSystem.current.currentSelectedGameObject == gameObject)
        {
            
        }
    }

    public void PressStart()
    {
        Debug.Log("Press Load");
        if (!firstPush)
        {
            Debug.Log("Go Saving Scene!");
            //�����Ɏ��̃V�[���ւ������߂�����
            SceneManager.LoadScene("mirror_room");
            SoundManager.Instance.PlaySE(1, 1f);
            firstPush = true;
        }
    }
}

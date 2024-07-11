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
    }

    private void Update()
    {
        if (_inputSetting.GetDecideKey() && EventSystem.current.currentSelectedGameObject == gameObject)
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
            SceneManager.LoadScene("ForVisualTeam");
            firstPush = true;
        }
    }
}

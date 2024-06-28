using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    private bool firstPush = false;

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

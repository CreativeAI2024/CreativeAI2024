using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    private bool firstPush = false;

    public void PressStart()
    {
        // �Q�l�ɂ�������@https://www.youtube.com/watch?v=gD0HvOg_i28&t=110s
        //�w�i�̈ꖇ�G�͎��o�ǂ�����炢���悢���
        //�^�C�g���̓Q�[���^�C�g�������܂肵�����A���o�ǂƑ��k
        Debug.Log("Press Start!");
        if (!firstPush)
        {
            Debug.Log("Go Next Scene!");
            //�����Ɏ��̃V�[���ւ������߂������B
            //SapmleScene�ɂ������ɂ��Ă���
            SceneManager.LoadScene("SampleScene");
            //
            firstPush = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

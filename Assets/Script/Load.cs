using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{

    private bool firstPush = false;

    public void PressStart()
    {
        // �Q�l�ɂ�������@https://www.youtube.com/watch?v=Y3lXlbJhO24

        Debug.Log("Press Load");
        if (!firstPush)
        {
            Debug.Log("Go Saving Scene!");
            //�����Ɏ��̃V�[���ւ������߂�����
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

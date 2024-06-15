using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        // GameManager�̃V���O���g���ݒ�
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //�V�[�����ړ�����Ƃ��ɁABGM�ύX����SE��炷
    //�V���O���g�������Ă���̂ŁAinstance�ŌĂяo��
    public void ToSampleScene()
    {
        SoundManager.instance.PlaySE(0);
        SoundManager.instance.PlayBGM("SampleScene");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayBGM("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
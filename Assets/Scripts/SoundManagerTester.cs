using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerTester : MonoBehaviour
{
    public SoundManager SoundManager;

    void Start()
    {
        // �Q�[���J�n���ɍŏ���BGM���Đ�
        SoundManager.PlayBGM("8-bit_Aggressive1", 1f);
    }

    // �V�[�����ړ�����Ƃ��ɁABGM��ύX����SE���Đ�
    public void ToSampleScene()
    {
        SoundManager.PlaySE("Pa", 0.5f); // SE���Đ�

        // �V�[����؂�ւ���
        SceneManager.LoadScene("SampleScene");

        // �؂�ւ�����ɕʂ�BGM���Đ�
        SoundManager.PlayBGM("Pappa_Parappa", 0.7f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

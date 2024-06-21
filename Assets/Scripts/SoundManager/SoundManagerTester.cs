using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class SoundManagerTester : MonoBehaviour
{
    void Start()
    {
        // �Q�[���J�n���ɍŏ���BGM���Đ�
        SoundManager.instance.PlayBGM(1, 1f);
    }

    // �V�[�����ړ�����Ƃ��ɁABGM��ύX����SE���Đ�
    public async void ToSampleScene()
    {
        SoundManager.instance.PlaySE(0, 0.5f); 

        // �V�[����񓯊��Ń��[�h����
        var loadScene = SceneManager.LoadSceneAsync("SampleScene");
        loadScene.allowSceneActivation = false;

        // �V�[���̃��[�h����������܂őҋ@
        while (loadScene.progress < 0.9f)
        {
            await Task.Yield(); // �t���[�����Ƃɑҋ@
        }


        // �V�[���̐؂�ւ�������
        loadScene.allowSceneActivation = true;

        // �V�[���؂�ւ����BGM���Đ��iindex 0��BGM���Đ��j
        SoundManager.instance.PlayBGM(0, 0.7f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBGM; // BGM�̃X�s�[�J�[
    public AudioClip[] audioClipsBGM;  // BGM�̉���

    public AudioSource audioSourceSE; // SE�̃X�s�[�J�[
    public AudioClip[] audioClipsSE;// SE�̉���

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayBGM(string sceneName)
    {
        AudioClip bgmClip = null;  

        switch (sceneName)
        {
            default:
            case "SampleScene":
                bgmClip = audioClipsBGM[0];
                break;
            case "SoundManager":
                bgmClip = audioClipsBGM[1];
               break;
        }

        // �����Đ�����BGM�������N���b�v�Ȃ�A�Đ����J�n���Ȃ�
        if(audioSourceBGM.clip == bgmClip && audioSourceBGM.isPlaying)
        {
            return;
        }

        audioSourceBGM.clip = bgmClip;
        audioSourceBGM.Play();
    }
    // SE����x�����Ȃ炷
    public void PlaySE(int index)
    {
       audioSourceSE.PlayOneShot(audioClipsSE[index]);
    }

    void Start()
    {
        
    }
   
    void Update()
    {

    }
}

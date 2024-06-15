using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBGM; // BGMのスピーカー
    public AudioClip[] audioClipsBGM;  // BGMの音源

    public AudioSource audioSourceSE; // SEのスピーカー
    public AudioClip[] audioClipsSE;// SEの音源

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

        // もし再生中のBGMが同じクリップなら、再生を開始しない
        if(audioSourceBGM.clip == bgmClip && audioSourceBGM.isPlaying)
        {
            return;
        }

        audioSourceBGM.clip = bgmClip;
        audioSourceBGM.Play();
    }
    // SEを一度だけならす
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

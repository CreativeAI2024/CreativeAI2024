using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourcesBGM; // BGMのスピーカー
    public List<AudioClip> audioClipsBGM;    // BGMの音源

    public AudioSource audioSourceSE; // SEのスピーカー
    public List<AudioClip> audioClipsSE; // SEの音源

    public static SoundManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            if(audioSourcesBGM != null)
            {
                audioSourcesBGM.loop = true;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void PlayBGM(int bgmIndex, float volume = 1f)
    {
        AudioClip bgmClip = audioClipsBGM[bgmIndex];
        CheckAudioClip(bgmIndex, bgmClip);

        // 同じBGMを再生している場合は何もしない
        if (audioSourcesBGM.clip == bgmClip)
        {
            return;
        }

        // 現在のBGMを停止して新しいBGMを再生
        StopBGM();

        // BGMを設定して再生
        audioSourcesBGM.clip = bgmClip;
        audioSourcesBGM.volume = volume;
        audioSourcesBGM.Play();
    
    }

    void StopBGM()
    {
        audioSourcesBGM.Stop();
    }

    public void PlaySE(int seIndex, float volume = 1f)
    {
        AudioClip seClip = audioClipsSE[seIndex];
        CheckAudioClip(seIndex, seClip);
        Debug.Log("Playing SE: " + seClip.name);
        audioSourceSE.PlayOneShot(seClip, volume);
    }

    void CheckAudioClip(int index, AudioClip clip)
    {
        CheckAudioClipRange(index);
        CheckAudioClipNull(clip);
    }

    void CheckAudioClipRange(int index)
    {
        if (index < 0 || index >= audioClipsBGM.Count)
        {
            throw new System.ArgumentOutOfRangeException(nameof(index));
        }
    }

    void CheckAudioClipNull(AudioClip clip)
    {
        if (clip == null)
        {
            throw new System.ArgumentNullException(nameof(clip), "not found: " );
        }
    }
}
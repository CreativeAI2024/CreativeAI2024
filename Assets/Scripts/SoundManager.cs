using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> audioSourcesBGM; // BGMのスピーカー（リスト）
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
        }
        else
        {
            Destroy(this.gameObject);
        }

        // BGM用のAudioSourceの数だけ初期化
        foreach (var source in audioSourcesBGM)
        {
            source.loop = true; // BGMはループする
        }
    }

    public void PlayBGM(string bgmName, float volume = 1f)
    {
        AudioClip bgmClip = audioClipsBGM.Find(clip => clip.name == bgmName);
        if (bgmClip == null)
        {
            Debug.LogWarning("BGM not found: " + bgmName);
            return;
        }

        // 使用可能なAudioSourceを取得
        AudioSource availableSource = GetAvailableBGMSource();
        if (availableSource == null)
        {
            Debug.LogWarning("No available BGM sources.");
            return;
        }

        // 同じBGMを再生している場合は何もしない
        if (availableSource.clip == bgmClip && availableSource.isPlaying)
        {
            return;
        }

        // BGMを設定して再生
        availableSource.clip = bgmClip;
        availableSource.volume = volume;
        availableSource.Play();
    }

    public void PlaySE(string seName, float volume = 1f)
    {
        AudioClip seClip = audioClipsSE.Find(clip => clip.name == seName);
        if (seClip == null)
        {
            Debug.LogWarning("SE not found: " + seName);
            return;
        }

        audioSourceSE.PlayOneShot(seClip, volume);
    }

    private AudioSource GetAvailableBGMSource()
    {
        foreach (var source in audioSourcesBGM)
        {
            if (source != null && !source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }


    void Start()
    {
        // ゲーム開始時に最初のBGMを再生
        PlayBGM("8-bit_Aggressive1", 1f);
    }

    void Update()
    {

    }
}

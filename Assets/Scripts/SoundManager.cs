using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    //AudioSourceBGMとAudioSourceSE一つずつ
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

            // BGM用のAudioSourceの数だけ初期化
            if (audioSourcesBGM == null)
            {
                audioSourcesBGM = new List<AudioSource>();
            }
            foreach (var source in audioSourcesBGM)
            {
                if (source != null)
                {
                    source.loop = true; // BGMはループする
                }
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void PlayBGM(int bgmIndex, float volume = 1f)
    {
        if (bgmIndex < 0 || bgmIndex >= audioClipsBGM.Count)
        {
            Debug.LogWarning("BGM index out of range: " + bgmIndex);
            return;
        }
        AudioClip bgmClip = audioClipsBGM[bgmIndex];
        if (bgmClip == null)
        {
            Debug.LogWarning("BGM not found: " + bgmIndex);
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

        // 現在のBGMを停止して新しいBGMを再生
        foreach (var source in audioSourcesBGM)
        {
            if (source != null && source.isPlaying)
            {
                source.Stop();
            }
        }

        // BGMを設定して再生
        availableSource.clip = bgmClip;
        availableSource.volume = volume;
        availableSource.Play();
    }


    public void PlaySE(int seIndex, float volume = 1f)
    {
        if (seIndex < 0 || seIndex >= audioClipsSE.Count)
        {
            Debug.LogWarning("SE index out of range: " + seIndex);
            return;
        }

        AudioClip seClip = audioClipsSE[seIndex];
        if (seClip == null)
        {
            Debug.LogWarning("SE not found: " + seIndex);
            return;
        }

        audioSourceSE.PlayOneShot(seClip, volume);
    }

    private AudioSource GetAvailableBGMSource()
    {
        // 最初に停止している（再生されていない）AudioSourceを探す
        foreach (var source in audioSourcesBGM)
        {
            if (source != null && !source.isPlaying)
            {
                return source;
            }
        }

        // 再生可能なAudioSourceが見つからない場合は、新しいAudioSourceを追加して返す
        AudioSource newSource = AddNewBGMSource();
        return newSource;
    }

    // 新しいBGM用のAudioSourceを追加する
    private AudioSource AddNewBGMSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.loop = true; // BGMはループする
        audioSourcesBGM.Add(newSource);
        return newSource;
    }

    //自分のゲームオブジェクトについてるAudioSourseを取得してリストに入れる。
    public List<AudioSource> GetBGMSources()
    {
        return audioSourcesBGM;
    }

    void Start()
    {
        // ゲーム開始時に最初のBGMを再生
        PlayBGM(1, 1f);
    }
}
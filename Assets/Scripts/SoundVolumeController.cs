using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class SoundVolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    string BGMLabel = "BGM";
    string SELabel = "SE";
    string MasterLabel = "Master";

    // 初期のデシベル値（BGM、SE、Masterの初期値を設定する場合に使用）
    float initialBGMVolume = 0f;
    float initialSEVolume = 0f;
    float initialMasterVolume = 0f;

    void Start()
    {
        // 各音量の初期値を設定
        audioMixer.GetFloat(BGMLabel, out initialBGMVolume);
        audioMixer.GetFloat(SELabel, out initialSEVolume);

        audioMixer.GetFloat(MasterLabel, out initialMasterVolume);

        // スライダーの値を初期化
        BGMSlider.value = DBToVolume(initialBGMVolume);
        SESlider.value = DBToVolume(initialSEVolume);
        masterVolumeSlider.value = DBToVolume(initialMasterVolume);

        // スライダーの値が変わったときに呼ばれるメソッドを設定
        BGMSlider.onValueChanged.AddListener(SetBGM);
        SESlider.onValueChanged.AddListener(SetSE);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    float DBToVolume(float dB)
    {
        // デシベル値が -80dB 未満にならないように制限する
        const float minDB = -80f;
        float clampedDB = Mathf.Clamp(dB, minDB, 0f); // 0f -> -80f の範囲にクランプ

        // デシベル値をパーセンテージに変換する
        return Mathf.Pow(10f, clampedDB / 20f);
    }

    float VolumeToDB(float volume)
    {
        // パーセンテージが 0 以下の場合、-80dB に制限する
        if (volume <= 0f)
            return -80f;
        else
            return Mathf.Log10(volume) * 20f;
    }

    public void SetBGM(float volume)
    {
        float dB = VolumeToDB(volume);
        StartCoroutine(SmoothChangeBGM(dB));
    }

    IEnumerator SmoothChangeBGM(float targetVolumeDB)
    {
        float currentVolumeDB;
        audioMixer.GetFloat(BGMLabel, out currentVolumeDB);

        float startVolumeDB = currentVolumeDB;
        float startTime = Time.unscaledTime;
        float endTime = startTime + 0.5f; // スライダーの操作にかかる時間（例えば0.5秒）

        while (Time.unscaledTime < endTime)
        {
            float t = (Time.unscaledTime - startTime) / (endTime - startTime);
            float newVolumeDB = Mathf.Lerp(startVolumeDB, targetVolumeDB, t);
            audioMixer.SetFloat(BGMLabel, newVolumeDB);
            yield return null;
        }

        // 最終的に確実に目標の音量にセットする
        audioMixer.SetFloat(BGMLabel, targetVolumeDB);
    }

    public void SetSE(float volume)
    {
        float dB = VolumeToDB(volume);
        audioMixer.SetFloat(SELabel, dB);
    }

    public void SetMasterVolume(float volume)
    {
        float dB = VolumeToDB(volume);
        audioMixer.SetFloat(MasterLabel, dB);
    }
}
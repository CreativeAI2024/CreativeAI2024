using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundVolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;

    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    void Start()
    {
        // BGMとSEの現在の音量を取得してスライダーの値を設定
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = dBToVolume(bgmVolume);

        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = dBToVolume(seVolume);

        audioMixer.GetFloat("Master", out float masterVolume);
        masterVolumeSlider.value = dBToVolume(masterVolume);

        // スライダーの値が変わったときに呼ばれるメソッドを設定
        BGMSlider.onValueChanged.AddListener(SetBGM);
        SESlider.onValueChanged.AddListener(SetSE);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);

        // スライダーの最小値を設定（例: 0.1）
        BGMSlider.minValue = 0.1f;
        SESlider.minValue = 0.1f;
        masterVolumeSlider.minValue = 0.1f;
    }

    float dBToVolume(float dB)
    {
        // デシベル値が -80dB 未満にならないように制限する
        const float minDB = -80f;
        float clampedDB = Mathf.Clamp(dB, minDB, 0f);

        // -Infinity の場合、最小のデシベル値に設定する
        if (float.IsNegativeInfinity(clampedDB))
        {
            clampedDB = minDB;
        }

        // デシベル値をパーセンテージに変換する
        return Mathf.Pow(10f, clampedDB / 20f);
    }




    public void SetBGM(float volume)
    {
        float dB = VolumeToDB(volume);
        audioMixer.SetFloat("BGM", dB);
    }

    public void SetSE(float volume)
    {
        float dB = VolumeToDB(volume);
        audioMixer.SetFloat("SE", dB);
    }

    public void SetMasterVolume(float volume)
    {
        float dB = VolumeToDB(volume);
        audioMixer.SetFloat("Master", dB);
    }

    float VolumeToDB(float volume)
    {
        // volume が 0 以下の場合、-80dB に制限する
        if (volume <= 0f)
            return -80f;
        else
            return Mathf.Log10(volume) * 20f;
    }




    void Update()
    {

    }
}
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
        // BGM��SE�̌��݂̉��ʂ��擾���ăX���C�_�[�̒l��ݒ�
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = dBToVolume(bgmVolume);

        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = dBToVolume(seVolume);

        audioMixer.GetFloat("Master", out float masterVolume);
        masterVolumeSlider.value = dBToVolume(masterVolume);

        // �X���C�_�[�̒l���ς�����Ƃ��ɌĂ΂�郁�\�b�h��ݒ�
        BGMSlider.onValueChanged.AddListener(SetBGM);
        SESlider.onValueChanged.AddListener(SetSE);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);

        // �X���C�_�[�̍ŏ��l��ݒ�i��: 0.1�j
        BGMSlider.minValue = 0.1f;
        SESlider.minValue = 0.1f;
        masterVolumeSlider.minValue = 0.1f;
    }

    float dBToVolume(float dB)
    {
        // �f�V�x���l�� -80dB �����ɂȂ�Ȃ��悤�ɐ�������
        const float minDB = -80f;
        float clampedDB = Mathf.Clamp(dB, minDB, 0f);

        // -Infinity �̏ꍇ�A�ŏ��̃f�V�x���l�ɐݒ肷��
        if (float.IsNegativeInfinity(clampedDB))
        {
            clampedDB = minDB;
        }

        // �f�V�x���l���p�[�Z���e�[�W�ɕϊ�����
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
        // volume �� 0 �ȉ��̏ꍇ�A-80dB �ɐ�������
        if (volume <= 0f)
            return -80f;
        else
            return Mathf.Log10(volume) * 20f;
    }




    void Update()
    {

    }
}
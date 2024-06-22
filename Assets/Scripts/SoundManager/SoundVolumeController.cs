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

    readonly string BGMLabel = "BGM";
    readonly string SELabel = "SE";
    readonly string MasterLabel = "Master";


    void Start()
    {
        // �X���C�_�[�̒l���ς�����Ƃ��ɌĂ΂�郁�\�b�h��ݒ�
        BGMSlider.onValueChanged.AddListener(SetBGM);
        SESlider.onValueChanged.AddListener(SetSE);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    float VolumeToDB(float volume)
    {
        // �p�[�Z���e�[�W�� 0 �ȉ��̏ꍇ�A-80dB �ɐ�������
        if (volume <= 0f)
            return -80f;
        else
            return Mathf.Log10(volume) * 20f;
    }

    private void SetVolume(string parameterLabel, float volume)
    {
        float dB = VolumeToDB(volume);
        Debug.Log("Setting " + parameterLabel + " volume to: " + dB);
        audioMixer.SetFloat(parameterLabel, dB);
    }
    public void SetBGM(float volume)
    {
        SetVolume(BGMLabel, volume);
    }

    public void SetSE(float volume)
    {
        SetVolume(SELabel, volume);
    }

    public void SetMasterVolume(float volume)
    {
        SetVolume(MasterLabel, volume);
    }
}
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

    // �����̃f�V�x���l�iBGM�ASE�AMaster�̏����l��ݒ肷��ꍇ�Ɏg�p�j
    float initialBGMVolume = 0f;
    float initialSEVolume = 0f;
    float initialMasterVolume = 0f;

    void Start()
    {
        // �e���ʂ̏����l��ݒ�
        audioMixer.GetFloat(BGMLabel, out initialBGMVolume);
        audioMixer.GetFloat(SELabel, out initialSEVolume);

        audioMixer.GetFloat(MasterLabel, out initialMasterVolume);

        // �X���C�_�[�̒l��������
        BGMSlider.value = DBToVolume(initialBGMVolume);
        SESlider.value = DBToVolume(initialSEVolume);
        masterVolumeSlider.value = DBToVolume(initialMasterVolume);

        // �X���C�_�[�̒l���ς�����Ƃ��ɌĂ΂�郁�\�b�h��ݒ�
        BGMSlider.onValueChanged.AddListener(SetBGM);
        SESlider.onValueChanged.AddListener(SetSE);
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    float DBToVolume(float dB)
    {
        // �f�V�x���l�� -80dB �����ɂȂ�Ȃ��悤�ɐ�������
        const float minDB = -80f;
        float clampedDB = Mathf.Clamp(dB, minDB, 0f); // 0f -> -80f �͈̔͂ɃN�����v

        // �f�V�x���l���p�[�Z���e�[�W�ɕϊ�����
        return Mathf.Pow(10f, clampedDB / 20f);
    }

    float VolumeToDB(float volume)
    {
        // �p�[�Z���e�[�W�� 0 �ȉ��̏ꍇ�A-80dB �ɐ�������
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
        float endTime = startTime + 0.5f; // �X���C�_�[�̑���ɂ����鎞�ԁi�Ⴆ��0.5�b�j

        while (Time.unscaledTime < endTime)
        {
            float t = (Time.unscaledTime - startTime) / (endTime - startTime);
            float newVolumeDB = Mathf.Lerp(startVolumeDB, targetVolumeDB, t);
            audioMixer.SetFloat(BGMLabel, newVolumeDB);
            yield return null;
        }

        // �ŏI�I�Ɋm���ɖڕW�̉��ʂɃZ�b�g����
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public List<AudioSource> audioSourcesBGM; // BGM�̃X�s�[�J�[�i���X�g�j
    public List<AudioClip> audioClipsBGM;    // BGM�̉���

    public AudioSource audioSourceSE; // SE�̃X�s�[�J�[
    public List<AudioClip> audioClipsSE; // SE�̉���

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

        // BGM�p��AudioSource�̐�����������
        foreach (var source in audioSourcesBGM)
        {
            source.loop = true; // BGM�̓��[�v����
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

        // �g�p�\��AudioSource���擾
        AudioSource availableSource = GetAvailableBGMSource();
        if (availableSource == null)
        {
            Debug.LogWarning("No available BGM sources.");
            return;
        }

        // ����BGM���Đ����Ă���ꍇ�͉������Ȃ�
        if (availableSource.clip == bgmClip && availableSource.isPlaying)
        {
            return;
        }

        // BGM��ݒ肵�čĐ�
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
        // �Q�[���J�n���ɍŏ���BGM���Đ�
        PlayBGM("8-bit_Aggressive1", 1f);
    }

    void Update()
    {

    }
}

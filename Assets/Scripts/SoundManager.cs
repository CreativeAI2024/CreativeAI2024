using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    //AudioSourceBGM��AudioSourceSE�����
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

            // BGM�p��AudioSource�̐�����������
            if (audioSourcesBGM == null)
            {
                audioSourcesBGM = new List<AudioSource>();
            }
            foreach (var source in audioSourcesBGM)
            {
                if (source != null)
                {
                    source.loop = true; // BGM�̓��[�v����
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

        // ���݂�BGM���~���ĐV����BGM���Đ�
        foreach (var source in audioSourcesBGM)
        {
            if (source != null && source.isPlaying)
            {
                source.Stop();
            }
        }

        // BGM��ݒ肵�čĐ�
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
        // �ŏ��ɒ�~���Ă���i�Đ�����Ă��Ȃ��jAudioSource��T��
        foreach (var source in audioSourcesBGM)
        {
            if (source != null && !source.isPlaying)
            {
                return source;
            }
        }

        // �Đ��\��AudioSource��������Ȃ��ꍇ�́A�V����AudioSource��ǉ����ĕԂ�
        AudioSource newSource = AddNewBGMSource();
        return newSource;
    }

    // �V����BGM�p��AudioSource��ǉ�����
    private AudioSource AddNewBGMSource()
    {
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.loop = true; // BGM�̓��[�v����
        audioSourcesBGM.Add(newSource);
        return newSource;
    }

    //�����̃Q�[���I�u�W�F�N�g�ɂ��Ă�AudioSourse���擾���ă��X�g�ɓ����B
    public List<AudioSource> GetBGMSources()
    {
        return audioSourcesBGM;
    }

    void Start()
    {
        // �Q�[���J�n���ɍŏ���BGM���Đ�
        PlayBGM(1, 1f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundVolumeControlle : MonoBehaviour
{
    //Audio�~�L�T�[������Ƃ���
    [SerializeField] AudioMixer audioMixer;

    //���ꂼ��̃X���C�_�[������Ƃ���
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    // Start is called before the first frame update
    void Start()
    {
        //�~�L�T�[��volume�ɃX���C�_�[��volume������

        //BGM
        audioMixer.GetFloat("BGM", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        //SE
        audioMixer.GetFloat("SE", out float seVolume);
        SESlider.value = seVolume;
    }
    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }

    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

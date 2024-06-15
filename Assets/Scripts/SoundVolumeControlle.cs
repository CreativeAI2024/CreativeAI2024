using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundVolumeControlle : MonoBehaviour
{
    //Audioミキサーを入れるところ
    [SerializeField] AudioMixer audioMixer;

    //それぞれのスライダーを入れるところ
    [SerializeField] Slider BGMSlider;
    [SerializeField] Slider SESlider;

    // Start is called before the first frame update
    void Start()
    {
        //ミキサーのvolumeにスライダーのvolumeを入れる

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

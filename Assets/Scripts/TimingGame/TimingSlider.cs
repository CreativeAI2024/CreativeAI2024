using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider: MonoBehaviour
{
    [HideInInspector] public Slider timingSlider;
    [SerializeField] private float upSpeed;
    private float saveUpSpeed;                  //upSpeedのバッファ用
    [HideInInspector] public float height;

    public void Initialize()
    {
        timingSlider = this.gameObject.GetComponent<Slider>();
        RectTransform sliderRectTransform = timingSlider.GetComponent<RectTransform>();
        height = sliderRectTransform.rect.height;
        timingSlider.value = 0;
    }

    public void ReInitialize()  //スライダーが停止された後繰り返すため
    {
        upSpeed = saveUpSpeed;
    }
    
    public void SliderMove()
    {
        timingSlider.value += upSpeed;
    }

    public void SliderStop()
    {
        saveUpSpeed = upSpeed;
        upSpeed = 0;
    }
}

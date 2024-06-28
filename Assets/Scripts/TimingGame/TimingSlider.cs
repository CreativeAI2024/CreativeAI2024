using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider: MonoBehaviour
{
    private Slider timingSlider;
    [SerializeField] private float upSpeed;
    private float saveUpSpeed; 

    private void Update()
    {
        timingSlider.value += upSpeed;
    }

    public void Initialize()
    {
        timingSlider = GetComponent<Slider>();
        timingSlider.value = 0;
    }

    public void ReInitialize()  //スライダーが停止された後繰り返すため
    {
        upSpeed = saveUpSpeed;
    }
    
    public void SliderStop()
    {
        saveUpSpeed = upSpeed;    //停止時にupSpeedが0になるので保存しておく
        upSpeed = 0;
    }

    public float SliderTopPosition()  //スライダーの上端の座標を返す
    {
        RectTransform sliderRectTransform = GetComponent<RectTransform>();
        return timingSlider.value * sliderRectTransform.rect.height;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider: MonoBehaviour
{
    private Slider timingSlider;
    [SerializeField, Range(0f, 1f)] private float ascendSpeed;
    private float ascend; 

    private void Update()
    {
        timingSlider.value += ascend;
    }

    public void Initialize()
    {
        timingSlider = GetComponent<Slider>();
        RestartSlider();
    }

    public void RestartSlider()  //スライダーが停止された後繰り返すため
    {
        timingSlider.value = 0;
        ascend = ascendSpeed;
    }
    
    public void SliderStop()
    {
        ascend = 0;
    }

    public float SliderTopPositionTime()  //スライダーの上端がいる座標へ到達するためにかかる時間を返す
    {
        return timingSlider.value / ascendSpeed;
    }
    public float SliderCoordinateSpeed()  //ascendSpeedを座標ベースでの速度に変換して返す
    {
        RectTransform sliderRectTransform = GetComponent<RectTransform>();
        return sliderRectTransform.rect.height * ascendSpeed;
    }
}

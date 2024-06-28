using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider: MonoBehaviour
{
    private Slider timingSlider;
    [SerializeField, HeaderAttribute("範囲:0-1")] private float ascendSpeed;
    private float ascend; 

    private void Update()
    {
        timingSlider.value += ascend;
    }

    public void Initialize()
    {
        timingSlider = GetComponent<Slider>();
        InitializeSliderParameter();
    }

    public void InitializeSliderParameter()  //スライダーが停止された後繰り返すため
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
    public float SliderCoordinateSpeed()  //スライダーの座標ベースでの速度を返す
    {
        RectTransform sliderRectTransform = GetComponent<RectTransform>();
        return sliderRectTransform.rect.height * ascendSpeed;
    }
}

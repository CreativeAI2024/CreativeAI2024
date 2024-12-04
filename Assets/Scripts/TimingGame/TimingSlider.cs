using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Slider))]
public class TimingSlider: MonoBehaviour
{
    [SerializeField] private RectTransform timingBar;
    private Slider timingSlider; 
    [SerializeField, Range(0f, 1f)] private float ascendSpeed;
    private float ascend;
    private float justTiming;

    public void Initialize()
    {
        timingSlider = GetComponent<Slider>();
        RestartSlider();

        justTiming = timingBar.anchoredPosition.y / SliderCoordinateSpeed();  //判定の基準となる時間
    }

    public void AscendSlider()
    {
        timingSlider.value += ascend * Time.deltaTime;
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
    private float JustTimingDiff()  //判定とのずれ(時間)を返す
    {
        return SliderTopPositionTime() - justTiming;
    }

    public float JustTimingDiffAbs() //判定とのずれ(時間)を絶対値で返す
    {

        return Math.Abs(JustTimingDiff());
    }
    private float SliderTopPositionTime()  //スライダーの上端がいる座標へ到達するためにかかる時間を返す
    {
        return timingSlider.value / ascendSpeed;
    }
    private float SliderCoordinateSpeed()  //ascendSpeedを座標ベースでの速度に変換して返す
    {
        RectTransform sliderRectTransform = GetComponent<RectTransform>();
        return sliderRectTransform.rect.height * ascendSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider: MonoBehaviour
{
    [HideInInspector] public Slider timingSlider;
    [SerializeField] private float upSpeed;
    private float saveUpSpeed;
    [HideInInspector] public float height;

    public void Initialize()
    {
        timingSlider = this.gameObject.GetComponent<Slider>();
        RectTransform sliderRectTransform = timingSlider.GetComponent<RectTransform>();
        height = sliderRectTransform.rect.height;
        timingSlider.value = 0;
    }

    public void ReInitialize()
    {
        upSpeed = saveUpSpeed;
    }
    

    public void ObjectMove()
    {
        timingSlider.value += upSpeed;
    }

    public void ObjectStop()
    {
        saveUpSpeed = upSpeed;
        upSpeed = 0;
    }
}

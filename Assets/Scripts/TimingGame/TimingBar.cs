using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingBar : MonoBehaviour
{
    [HideInInspector] public RectTransform timingBar;
    [HideInInspector] public float justTiming;

    public void Initialize()
    {
        timingBar = this.gameObject.GetComponent<RectTransform>();
        justTiming = timingBar.anchoredPosition.y;
    }
}

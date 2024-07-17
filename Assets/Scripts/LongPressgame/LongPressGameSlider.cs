using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LongPressGameSlider : MonoBehaviour
{
    [SerializeField] private Slider longPressGameSlider;

    [SerializeField] private TextMeshProUGUI pressTimeText;
    [SerializeField] private TextMeshProUGUI goalTimeText;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        longPressGameSlider = GetComponent<Slider>();
        longPressGameSlider.value = 0;
    }

    public void SetPressCountText(string text)
    {
        if (pressTimeText != null)
        {
            pressTimeText.text = text;
        }
    }

    public void SetGoalTimeText(string text)
    {
        if (goalTimeText != null)
        {
            goalTimeText.text = text;
        }
    }

    public void SetSliderValue(float value)
    {
        longPressGameSlider.value = value;
    }
}

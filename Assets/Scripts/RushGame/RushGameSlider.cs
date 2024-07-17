using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RushGameSlider : MonoBehaviour
{
    [SerializeField] private Slider RushgameSlider;

    [SerializeField] private TextMeshProUGUI timeLimit;
    [SerializeField] private TextMeshProUGUI pressCountText;
    [SerializeField] private TextMeshProUGUI goalCountText;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        RushgameSlider = GetComponent<Slider>();
        RushgameSlider.value = 0;
    }

    public void SetTimeLimit(string text)
    {
        if (timeLimit != null)
        {
            timeLimit.text = text;
        }
    }

    public void SetPressCountText(string text)
    {
        if (pressCountText != null)
        {
            pressCountText.text = text;
        }
    }

    public void SetGoalCountText(string text)
    {
        if (goalCountText != null)
        {
            goalCountText.text = text;
        }
    }

    public void SetSliderValue(float value)
    {
        RushgameSlider.value = value;
    }

}

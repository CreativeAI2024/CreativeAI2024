using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingObject : MonoBehaviour
{
    RectTransform timingObject;
    float up;
    public float upSpeed;
    private InputSetting _inputSetting;
    public float top;

    public void Initialize()
    {
        timingObject = this.gameObject.GetComponent<RectTransform>();
        up = timingObject.offsetMax.y;
        _inputSetting = InputSetting.Load();
    }

    public void ObjectMove()
    {
        up += upSpeed;

        timingObject.offsetMax = new Vector2(timingObject.offsetMax.x, up);
        top = -timingObject.offsetMax.y;
    }

    public void ObjectStop()
    {
        if (_inputSetting.GetDecideKeyUp())
        {
            upSpeed = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchGameCursor : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private InputSetting _inputSetting;
    public float speed = 300.0f;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    void Update()
    {
        if (_inputSetting.GetForwardKey())
        {
            transform.position += speed * Time.deltaTime * Vector3.up;
        }
        if (_inputSetting.GetBackKey())
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
        }
        if (_inputSetting.GetLeftKey())
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
        }
        if (_inputSetting.GetRightKey())
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }
    }

    private bool IsOnUpEdge()
    {
        return true;
    }
}

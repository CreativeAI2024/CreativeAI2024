using UnityEngine;

public class SearchGameCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform cursor;
    [SerializeField] private RectTransform judgePoint;
    [SerializeField] private EventTriggerImages eventTriggerImages;
    private const float speed = 300.0f;
    private float cursorTipOffsetX;
    private float cursorTipOffsetY;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        cursorTipOffsetX = cursor.rect.height/2 - judgePoint.anchoredPosition.x;
        cursorTipOffsetY = cursor.rect.width/2 - judgePoint.anchoredPosition.y;
    }

    void Update()
    {
        if (_inputSetting.GetForwardKey() && !IsOnUpEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.up;
        }
        if (_inputSetting.GetBackKey() && !IsOnDownEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
        }
        if (_inputSetting.GetLeftKey() && !IsOnLeftEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
        }
        if (_inputSetting.GetRightKey() && !IsOnRightEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }
    }
    private bool IsOnUpEdge()
    {
        return cursor.anchoredPosition.y - cursorTipOffsetY > canvas.rect.height;
    }
    private bool IsOnDownEdge()
    {
        return cursor.anchoredPosition.y - cursorTipOffsetY < 0;
    }
    private bool IsOnLeftEdge()
    {
        return cursor.anchoredPosition.x - cursorTipOffsetX < 0;
    }
    private bool IsOnRightEdge()
    {
        return cursor.anchoredPosition.x - cursorTipOffsetX > canvas.rect.width;
    }
}

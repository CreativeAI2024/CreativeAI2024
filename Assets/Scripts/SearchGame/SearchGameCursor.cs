using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class SearchGameCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private Renderer searchGame;
    [SerializeField] private SpriteRenderer cursorImage;
    [SerializeField] private Sprite arrowCursor;
    [SerializeField] private Sprite handCursor;
    [SerializeField] private Transform cursorTip;
    private Camera mainCamera;
    [SerializeField] private float speed = 3.0f;
    private bool isFocusing = false;
    private bool isInputModeMouse = false;
    private Vector3 lastMousePosition;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        mainCamera = Camera.main;
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        //TODO: マウスカーソル受け付けるようにする
        if (!isInputModeMouse && lastMousePosition != Input.mousePosition)
        {
            isInputModeMouse = true;
        }
        else if (Input.anyKeyDown)
        {
            isInputModeMouse = false;
        }

        if (isInputModeMouse)
        {
            lastMousePosition = Input.mousePosition;
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(lastMousePosition.x, lastMousePosition.y, -mainCamera.transform.position.z));
        }
        else
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
    }
    public void SetIsFocusing(bool isFocusing)
    {
        this.isFocusing = isFocusing;
        cursorImage.sprite = isFocusing ? handCursor : arrowCursor;
    }
    private bool IsOnUpEdge()
    {
        return cursorTip.position.y > searchGame.bounds.max.y;
    }
    private bool IsOnDownEdge()
    {
        return cursorTip.position.y < searchGame.bounds.min.y;
    }
    private bool IsOnLeftEdge()
    {
        return cursorTip.position.x < searchGame.bounds.min.x;
    }
    private bool IsOnRightEdge()
    {
        return cursorTip.position.x > searchGame.bounds.max.x;
    }
}

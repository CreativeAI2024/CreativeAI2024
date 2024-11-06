using UnityEngine;

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
            if (_inputSetting.GetForwardKey() && !IsOutUpEdge())
            {
                transform.position += speed * Time.deltaTime * Vector3.up;
            }
            if (_inputSetting.GetBackKey() && !IsOutDownEdge())
            {
                transform.position += speed * Time.deltaTime * Vector3.down;
            }
            if (_inputSetting.GetLeftKey() && !IsOutLeftEdge())
            {
                transform.position += speed * Time.deltaTime * Vector3.left;
            }
            if (_inputSetting.GetRightKey() && !IsOutRightEdge())
            {
                transform.position += speed * Time.deltaTime * Vector3.right;
            }
        }
    }
    public void SetIsFocusing(bool isFocusing)
    {
        cursorImage.sprite = isFocusing ? handCursor : arrowCursor;
    }
    public void Reset()
    {
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, -mainCamera.transform.position.z));
        isInputModeMouse = false;
    }
    private bool IsOutUpEdge()
    {
        return cursorTip.position.y > searchGame.bounds.max.y;
    }
    private bool IsOutDownEdge()
    {
        return cursorTip.position.y < searchGame.bounds.min.y;
    }
    private bool IsOutLeftEdge()
    {
        return cursorTip.position.x < searchGame.bounds.min.x;
    }
    private bool IsOutRightEdge()
    {
        return cursorTip.position.x > searchGame.bounds.max.x;
    }
}

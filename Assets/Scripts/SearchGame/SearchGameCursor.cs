using UnityEngine;

public class SearchGameCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private SpriteRenderer cursorImage;
    [SerializeField] private Sprite arrowCursor;
    [SerializeField] private Sprite handCursor;
    [SerializeField] private Transform cursorTip;
    private Vector3 tipOffset;
    private Camera mainCamera;
    [SerializeField] private float speed = 3.0f;
    private bool isInputModeMouse = false;
    private Vector2 leftDown;
    private Vector2 rightUp;
    private Vector3 lastMousePosition;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        tipOffset = cursorTip.position-transform.position;
        mainCamera = Camera.main;
        leftDown = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        rightUp = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -mainCamera.transform.position.z));
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
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
            transform.position = GetNewCursorPosition(mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z)));
            lastMousePosition = Input.mousePosition;
        }
        else
        {
            Vector3 moveDirection = Vector3.zero;
            if (_inputSetting.GetForwardKey())
            {
                moveDirection += Vector3.up;
            }
            if (_inputSetting.GetBackKey())
            {
                moveDirection += Vector3.down;
            }
            if (_inputSetting.GetLeftKey())
            {
                moveDirection += Vector3.left;
            }
            if (_inputSetting.GetRightKey())
            {
                moveDirection += Vector3.right;
            }
            transform.position = GetNewCursorPosition(transform.position+tipOffset + speed * Time.deltaTime * moveDirection);
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
    private Vector3 GetNewCursorPosition(Vector3 newPosition)
    {
            float xPosition = Mathf.Clamp(newPosition.x, leftDown.x, rightUp.x);
            float yPosition = Mathf.Clamp(newPosition.y, leftDown.y, rightUp.y);
            return new Vector3(xPosition-tipOffset.x, yPosition-tipOffset.y, 0);

    }
}

using UnityEngine;

public class SearchGameCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private Renderer searchGame;
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
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
            float xPosition = Mathf.Clamp(mousePosition.x+tipOffset.x, leftDown.x, rightUp.x);
            float yPosition = Mathf.Clamp(mousePosition.y+tipOffset.y, leftDown.y, rightUp.y);
            transform.position = new Vector3(xPosition-tipOffset.x, yPosition-tipOffset.y, 0);
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
            float xPosition = Mathf.Clamp(transform.position.x+tipOffset.x + speed * Time.deltaTime * moveDirection.x, leftDown.x, rightUp.x);
            float yPosition = Mathf.Clamp(transform.position.y+tipOffset.y + speed * Time.deltaTime * moveDirection.y, leftDown.y, rightUp.y);
            transform.position = new Vector3(xPosition-tipOffset.x, yPosition-tipOffset.y, 0);
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
}

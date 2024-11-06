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
    private Vector2 leftdown;
    private Vector2 rightup;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        lastMousePosition = Input.mousePosition;
        mainCamera = Camera.main;
        leftdown = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        rightup = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -mainCamera.transform.position.z));
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
            float xPosition = Mathf.Clamp(lastMousePosition.x, 0, Screen.width);
            float yPosition = Mathf.Clamp(lastMousePosition.y, 0, Screen.height);
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(xPosition, yPosition, -mainCamera.transform.position.z));
        }
        else
        {
            Vector3 moveDirection = Vector3.zero;
            if (_inputSetting.GetForwardKey())
            {
                moveDirection = Vector3.up;
            }
            if (_inputSetting.GetBackKey())
            {
                moveDirection = Vector3.down;
            }
            if (_inputSetting.GetLeftKey())
            {
                moveDirection = Vector3.left;
            }
            if (_inputSetting.GetRightKey())
            {
                moveDirection = Vector3.right;
            }
            float x = Mathf.Clamp(transform.position.x + speed * Time.deltaTime * moveDirection.x, leftdown.x, rightup.x);
            float y = Mathf.Clamp(transform.position.y + speed * Time.deltaTime * moveDirection.y, leftdown.y, rightup.y);
            transform.position = new Vector3(x, y, 0);
        }
    }
    public void SetIsFocusing(bool isFocusing)
    {
        cursorImage.sprite = isFocusing ? handCursor : arrowCursor;
    }
    public void Reset()
    {
        transform.position = Vector3.zero;
        isInputModeMouse = false;
    }
}

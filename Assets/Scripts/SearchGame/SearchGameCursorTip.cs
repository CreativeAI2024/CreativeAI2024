using UnityEngine;

public class SearchGameCursorTip : MonoBehaviour
{
    private InputSetting _inputSetting;
    private Camera mainCamera;
    [SerializeField] private float speed = 3.0f;
    private Vector2 leftDown;
    private Vector2 rightUp;
    private Vector3 lastMousePosition;
    [SerializeField] private SearchGameCursor cursor;

    void Start()
    {
        _inputSetting = InputSetting.Load();
        mainCamera = Camera.main;
        leftDown = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));
        rightUp = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -mainCamera.transform.position.z));
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 newCursorTipPosition;
        if (lastMousePosition != Input.mousePosition)
        {
            Vector3 currentMousePosition = new(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z);
            lastMousePosition = Input.mousePosition;
            newCursorTipPosition = mainCamera.ScreenToWorldPoint(currentMousePosition);
            transform.position = ClampCursorTipPosition(newCursorTipPosition);
        }
        else if (Input.anyKey)
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
            newCursorTipPosition = transform.position + speed * Time.deltaTime * moveDirection;
            transform.position = ClampCursorTipPosition(newCursorTipPosition);
        }
    }

    public void Reset()
    {
        Vector3 screenCenter = new(Screen.width / 2, Screen.height / 2, -mainCamera.transform.position.z);
        transform.position = mainCamera.ScreenToWorldPoint(screenCenter);
    }

    private Vector3 ClampCursorTipPosition(Vector3 newCursorTipPosition)
    {
        float xPosition = Mathf.Clamp(newCursorTipPosition.x, leftDown.x, rightUp.x);
        float yPosition = Mathf.Clamp(newCursorTipPosition.y, leftDown.y, rightUp.y);
        return new Vector3(xPosition, yPosition, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        cursor.SetIsFocusing(true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        cursor.SetIsFocusing(false);
    }
}

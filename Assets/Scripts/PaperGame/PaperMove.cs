using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Camera _mainCamera;
    private Vector3 _offset;
    [SerializeField] private float speed;
    private InputSetting _inputSetting;
    private Vector2 _imageSize;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (_inputSetting.GetForwardKey())
            moveDirection += Vector3.up;
        if (_inputSetting.GetBackKey())
            moveDirection += Vector3.down;
        if (_inputSetting.GetLeftKey())
            moveDirection += Vector3.left;
        if (_inputSetting.GetRightKey())
            moveDirection += Vector3.right;

        if (_inputSetting.GetCancelKey())
        {
            //SceneManager.LoadScene("");  //仮置き
        }

        Vector3 newPosition = transform.position + speed * Time.deltaTime * moveDirection.normalized;
        transform.position = ClampToScreen(newPosition);
    }

    public void Initialize()
    {
        _mainCamera = Camera.main;
        _inputSetting = InputSetting.Load();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Rect spriteRect = spriteRenderer.sprite.rect;

        _imageSize.x = spriteRect.width;  // 画像のサイズ（幅、高さ）を取得
        _imageSize.y = spriteRect.height;  // 画像のサイズ（幅、高さ）を取得
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z) - _offset;
        transform.position = ClampToScreen(newPosition);
    }

    private Vector3 ClampToScreen(Vector3 position)
    {
        Vector3 minScreenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -_mainCamera.transform.position.z));
        Vector3 maxScreenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -_mainCamera.transform.position.z));

        float clampedX = Mathf.Clamp(position.x, 0 + _imageSize.x / 2, Screen.width - _imageSize.x / 2);
        float clampedY = Mathf.Clamp(position.y, 0 + _imageSize.y / 2, Screen.height - _imageSize.y / 2);

        return new Vector3(clampedX, clampedY, position.z);
    }
}

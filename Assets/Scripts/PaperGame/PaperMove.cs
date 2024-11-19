using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Camera _mainCamera;
    private Vector3 _offset;
    [SerializeField] private float speed;
    private InputSetting _inputSetting;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _imageSize;

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

        Vector3 newPosition = transform.position + moveDirection * speed * Time.deltaTime;
        transform.position = ClampToScreen(newPosition);
    }

    public void Initialize()
    {
        _mainCamera = Camera.main;
        _inputSetting = InputSetting.Load();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = _spriteRenderer.sprite;
        //_imageSize = _spriteRenderer.bounds.size / transform.localScale;  // 画像のサイズ（幅、高さ）を取得
        //DebugLogger.Log((_spriteRenderer.bounds.size.x) + ":" + (_spriteRenderer.bounds.size.y));

        _imageSize.x = sprite.rect.width;  // 画像のサイズ（幅、高さ）を取得
        _imageSize.y = sprite.rect.height;  // 画像のサイズ（幅、高さ）を取得
        //DebugLogger.Log((_imageSize.x) + ":" + (_imageSize.y));


    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z));
        //_offset = transform.position - mouseWorldPos;
        _offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z));
        Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, -_mainCamera.transform.position.z) - _offset;
        transform.position = ClampToScreen(newPosition);
    }

    private Vector3 ClampToScreen(Vector3 position)
    {
        Vector3 minScreenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, -_mainCamera.transform.position.z));
        Vector3 maxScreenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -_mainCamera.transform.position.z));

        //float clampedX = Mathf.Clamp(position.x, minScreenBounds.x + _imageSize.x / 2, maxScreenBounds.x - _imageSize.x / 2);
        //float clampedY = Mathf.Clamp(position.y, minScreenBounds.y + _imageSize.y / 2, maxScreenBounds.y - _imageSize.y / 2);
        float clampedX = Mathf.Clamp(position.x, 0 + _imageSize.x / 2, Screen.width - _imageSize.x / 2);
        float clampedY = Mathf.Clamp(position.y, 0 + _imageSize.y / 2, Screen.height - _imageSize.y / 2);
        //DebugLogger.Log((minScreenBounds.x) + ":" + (maxScreenBounds.x));

        return new Vector3(clampedX, clampedY, position.z);
    }
}

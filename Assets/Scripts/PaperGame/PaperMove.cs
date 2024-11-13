using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _previousPos = Vector3.zero;
    private Vector3 _initialPos;
    private RectTransform _rectTransform;

    [SerializeField] private float speed;
    private InputSetting _inputSetting;
    private RectTransform _canvasRectTransform;

    private void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
        _inputSetting = InputSetting.Load();

        _canvasRectTransform = _rectTransform.root.GetComponent<RectTransform>();
    }

    private void Update()
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

        Vector3 newPosition = _rectTransform.localPosition + moveDirection * speed * Time.deltaTime;

        Vector3 clampedPosition = ClampToCanvas(newPosition);
        _rectTransform.localPosition = clampedPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousPos = eventData.position;
        _initialPos = _rectTransform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 _currentPos = eventData.position;
        Vector3 diff = _currentPos - _previousPos;
        _rectTransform.localPosition = _initialPos + new Vector3(diff.x, diff.y);

        _rectTransform.localPosition = ClampToCanvas(_rectTransform.localPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    private Vector3 ClampToCanvas(Vector3 position)
    {
        Vector3 minPosition = _canvasRectTransform.rect.min - _rectTransform.rect.min;
        Vector3 maxPosition = _canvasRectTransform.rect.max - _rectTransform.rect.max;

        float clampedX = Mathf.Clamp(position.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(position.y, minPosition.y, maxPosition.y);

        return new Vector3(clampedX, clampedY, position.z);
    }
}

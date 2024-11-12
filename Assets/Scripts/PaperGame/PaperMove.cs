using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _previousPos = Vector3.zero;
    private Vector3 _initialPos;
    private RectTransform _rectTransform;

    [SerializeField] private float speed;

    private void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right;
        }

        _rectTransform.position += moveDirection * speed * Time.deltaTime;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousPos = eventData.position;
        _initialPos = _rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        Vector3 _currentPos = eventData.position;
        Vector3 diff = _currentPos - _previousPos;
        _rectTransform.position = _initialPos + new Vector3(diff.x, diff.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}

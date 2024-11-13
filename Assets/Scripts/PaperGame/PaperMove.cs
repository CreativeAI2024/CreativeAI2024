using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _previousPos = Vector3.zero;
    private Vector3 _initialPos;
    private Transform _rectTransform;

    [SerializeField] private float speed;

    private void Start()
    {
        _rectTransform = this.GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
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

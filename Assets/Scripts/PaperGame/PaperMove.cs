using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _previousPos = Vector3.zero;
    private Vector3 _initialPos;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = this.GetComponent<RectTransform>();
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

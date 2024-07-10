using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _previousPos = Vector3.zero;
    private Vector3 _initialPos;
    private bool _isDrag = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousPos = eventData.position;
        _initialPos = this.transform.GetComponent<RectTransform>().position;
        _isDrag = true;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if (_isDrag)
        {
            Vector3 _currentPos = eventData.position;
            Vector3 _diffDistance = _currentPos - _previousPos;
            this.transform.GetComponent<RectTransform>().position = _initialPos + new Vector3(_diffDistance.x, _diffDistance.y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDrag = false;
    }
}

using UnityEngine;

public class PaperMove: MonoBehaviour
{
    private Vector3 _previousPos = Vector3.zero; 
    private Vector3 _currentPos = Vector3.zero;  
    private bool _isDrag = false; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _previousPos = Input.mousePosition;
        }

        if (_isDrag)
        {
            if (Input.GetMouseButton(0))
            {
                _currentPos = Input.mousePosition;
                Vector3 _diffDistance = _currentPos - _previousPos;

                this.transform.GetComponent<RectTransform>().position += new Vector3(_diffDistance.x, _diffDistance.y);
                _previousPos = Input.mousePosition;
            }
        }
    }

    public void OnDrag()
    {
        _isDrag = true;
    }

    public void OffDrag()
    {
        _isDrag = false;
    }
}
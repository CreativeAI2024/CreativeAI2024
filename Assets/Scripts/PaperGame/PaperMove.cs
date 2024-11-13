using UnityEngine;
using UnityEngine.EventSystems;

public class PaperMove : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _previousPos = Vector3.zero;
    private Vector3 _initialPos;
    private Transform _transform;

    [SerializeField] private float speed;

    private InputSetting _inputSetting;

    private void Start()
    {
        _transform = this.GetComponent<Transform>();

        _inputSetting = InputSetting.Load();
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

        // 紙オブジェクトをキー入力に基づいて移動
        _transform.position += moveDirection * speed * Time.deltaTime;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousPos = eventData.position;
        _initialPos = _transform.position;
    }

    public void OnDrag(PointerEventData eventData) 
    {
        Vector3 _currentPos = eventData.position;
        Vector3 diff = _currentPos - _previousPos;
        _transform.position = _initialPos + new Vector3(diff.x, diff.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}

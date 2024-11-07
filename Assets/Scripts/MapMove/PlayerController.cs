using UnityEngine;

// CollisionEnterを機能させるために必要
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    public float allowDistance = 0.03f;
    private bool _canInput = true;
    private Vector2Int _targetPosition = Vector2Int.zero;
    private Vector2Int _startPosition = Vector2Int.zero;
    private Transform _playerTransform;
    [SerializeField] private TileInfo tileInfo;
    private InputSetting _inputSetting;
    private Vector2Int _lastinputVector;

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        _inputSetting = InputSetting.Load();
        _playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canInput)
        {
            _lastinputVector = GetInputVector();
            if (_lastinputVector != Vector2Int.zero)
            {
                _canInput = false;
            }
            _startPosition = tileInfo.GridPosition;
            _targetPosition = _startPosition + _lastinputVector;
        }
        else
        {
            Move(new Vector3(_lastinputVector.x, _lastinputVector.y, 0));
            MoveEnd();
        }

    }

    Vector2Int GetInputVector()
    {
        Vector2Int vector = Vector2Int.zero;
        if (_inputSetting.GetForwardKey())
        {
            vector += Vector2Int.up;
        }
        if (_inputSetting.GetLeftKey())
        {
            vector += Vector2Int.left;
        }
        if (_inputSetting.GetBackKey())
        {
            vector += Vector2Int.down;
        }
        if (_inputSetting.GetRightKey())
        {
            vector += Vector2Int.right;
        }
        Vector2Int result = vector.x * vector.x + vector.y * vector.y != 1 ?
            Vector2Int.zero : vector;
        return result;
    }

    protected virtual void Move(Vector3 vector)
    {
        _playerTransform.position += vector * (Time.deltaTime * speed);
    }

    void MoveEnd()
    {
        Vector3 targetVector = new Vector3(_targetPosition.x, _targetPosition.y, 0);
        if (Vector3.Distance(targetVector, _playerTransform.position) >= allowDistance) return;
        
        _playerTransform.position = targetVector;
        MovePrepare();
    }

    protected virtual void MovePrepare()
    {
        _canInput = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ResetPosition();
    }

    protected void ResetPosition()
    {
        _playerTransform.position = new Vector3(_startPosition.x, _startPosition.y, 0);
        MovePrepare();
    } 
}

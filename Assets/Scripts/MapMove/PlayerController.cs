using UnityEngine;
using UnityEngine.SceneManagement;

// CollisionEnterを機能させるために必要
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private MapDataController mapDataController;
    public float speed = 3f;
    public float allowDistance = 0.03f;
    private bool _canInput = true;
    private Vector2Int _targetPosition = Vector2Int.zero;
    private Vector2Int _startPosition = Vector2Int.zero;
    private Transform _playerTransform;
    private InputSetting _inputSetting;
    public Vector2Int LastInputVector { get; private set; }

    public Vector2Int Direction { get; private set; }
    private string startSceneName;

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        _inputSetting = InputSetting.Load();
        _playerTransform = transform;
        startSceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (_canInput)
        {
            LastInputVector = GetInputVector();
            _startPosition = GetGridPosition();
            if (LastInputVector != Vector2Int.zero)
            {
                Direction = LastInputVector;
                _canInput = mapDataController.IsGridPositionOutOfRange(_startPosition + LastInputVector);
            }
            _targetPosition = mapDataController.ConvertGridPosition(_startPosition + LastInputVector);
        }
        else
        {
            Move();
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

    protected virtual void Move()
    {
        Vector3 targetPosition = _playerTransform.position + new Vector3(LastInputVector.x, LastInputVector.y, 0);
        _playerTransform.position = Vector3.MoveTowards(_playerTransform.position, targetPosition, Time.deltaTime * speed);
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

    public Vector2Int GetGridPosition()
    {
        if (startSceneName == SceneManager.GetActiveScene().name)
        {
            return new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
        return new();
    }
}

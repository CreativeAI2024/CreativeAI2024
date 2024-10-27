using UnityEngine;

// CollisionEnterを機能させるために必要
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float unitDistance = 1f;
    public float speed = 3f;
    public float allowDistance = 0.03f;
    private bool _canInput = true;
    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _startPosition = Vector3.zero;
    private Transform _playerTransform;
    private InputSetting _inputSetting;
    private Vector3 _lastinputVector;

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
            if (_lastinputVector != Vector3.zero)
            {
                _canInput = false;
            }
            _startPosition = _playerTransform.position;
            _targetPosition = _startPosition + _lastinputVector;
        }
        else
        {
            Move(_lastinputVector);
            MoveEnd();
        }

    }

    Vector3 GetInputVector()
    {
        Vector3 vector = Vector3.zero;
        if (_inputSetting.GetForwardKey())
        {
            vector += Vector3.up;
        }
        if (_inputSetting.GetLeftKey())
        {
            vector += Vector3.left;
        }
        if (_inputSetting.GetBackKey())
        {
            vector += Vector3.down;
        }
        if (_inputSetting.GetRightKey())
        {
            vector += Vector3.right;
        }
        return vector.normalized * unitDistance;
    }

    protected virtual void Move(Vector3 vector)
    {
        _playerTransform.position += Time.deltaTime * 100 * vector.normalized * speed;
    }

    void MoveEnd()
    {
        if (Vector3.Distance(_targetPosition, _playerTransform.position) >= allowDistance) return;
        
        _playerTransform.position = _targetPosition;
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
        _playerTransform.position = _startPosition;
        MovePrepare();
    } 
}

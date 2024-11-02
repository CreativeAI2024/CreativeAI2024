using UnityEngine;
using UnityEngine.Serialization;

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
    public Vector3 LastInputVector { get; private set; }

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
            if (GetInputVector() != LastInputVector)
            {
                _playerTransform.position = _targetPosition;
            }
            else
            {
                Move(LastInputVector);
            }
            LastInputVector = GetInputVector();
            if (LastInputVector != Vector3.zero)
            {
                _canInput = false;
            }
            _startPosition = _playerTransform.position;
            _targetPosition = _startPosition + LastInputVector;
        }
        else
        {
            Move(LastInputVector);
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
        
        Vector3 result = Mathf.RoundToInt(vector.sqrMagnitude * 10) / 10 != 1 ? 
            Vector3.zero : 
            vector * unitDistance;
        return result;
    }

    protected virtual void Move(Vector3 vector)
    {
        _playerTransform.position += vector.normalized * (Time.deltaTime * speed);
    }

    void MoveEnd()
    {
        if (Vector3.Distance(_targetPosition, _playerTransform.position) >= allowDistance) return;
        /*
        if (GetInputVector() == LastInputVector)
        {
            _targetPosition = _startPosition + LastInputVector;
            return;
        }*/
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

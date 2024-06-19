using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float unitDistance = 1f;
    public float speed = 3f;
    public float allowDistance = 0.05f;
    private bool _canMove = true;
    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _startPosition = Vector3.zero;
    private Rigidbody2D _myRigidbody;
    private Transform _playerTransform;
    private InputSetting _inputSetting;
    
    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _inputSetting = InputSetting.Load();
        _playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            Vector3 inputVector = GetInputVector();
            if (inputVector != Vector3.zero)
            {
                _canMove = false;
            }
            InputMove(inputVector);
        }
        MoveEnd();
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
        vector *= unitDistance;
        return vector;
    }

    protected virtual void InputMove(Vector3 vector)
    {
        _startPosition = _playerTransform.position;
        _targetPosition = _startPosition + vector;
        _myRigidbody.velocity = vector * speed;
    }

    void MoveEnd()
    {
        if (_canMove) return;
        if (Vector3.Distance(_targetPosition, _playerTransform.position) >= allowDistance) return;
        
        _playerTransform.position = _targetPosition;
        MovePrepare();
    }

    protected virtual void MovePrepare()
    {
        _myRigidbody.velocity = Vector3.zero;
        _canMove = true;
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

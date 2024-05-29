using UnityEngine;

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
    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();
        MoveEnd();
    }

    void InputMove()
    {
        if (!_canMove) return;
        
        Vector3 vector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            vector += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vector += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vector += Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vector += Vector3.right;
        }
        if (vector != Vector3.zero)
        {
            _canMove = false;
        }
        vector *= unitDistance;
        _startPosition = _playerTransform.position;
        _targetPosition = _startPosition + vector;
        _myRigidbody.velocity = vector * speed;
    }

    void MoveEnd()
    {
        if (_canMove) return;
        if (Vector3.Distance(_targetPosition, _playerTransform.position) >= allowDistance) return;

        MovePrepare(_targetPosition);
    }

    void MovePrepare(Vector3 position)
    {
        _playerTransform.position = position;
        _myRigidbody.velocity = Vector3.zero;
        _canMove = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        MovePrepare(_startPosition);
    }
}

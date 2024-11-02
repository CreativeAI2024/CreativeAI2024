using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMoveVisual : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [FormerlySerializedAs("characterSprites")] [SerializeField] private Sprite[] characterIdleSprites;
    [SerializeField] private Sprite[] characterWalkSprites;
    [SerializeField] private Sprite[] characterWalkSprites1;
    [SerializeField] private float timeClockInterval = 0.2f;
    private Sprite[][] _characterWalkSprites;
    private int _walkClock = 0;
    private float _timer = 0;
    Vector3 _lastPosition;
    int _lastDirection = 0;
    // Start is called before the first frame update
    void Start()
    {
        _characterWalkSprites = new Sprite[2][];
        _characterWalkSprites[0] = characterWalkSprites;
        _characterWalkSprites[1] = characterWalkSprites1;
        _lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector = (transform.position - _lastPosition).normalized;
        _lastPosition = transform.position;
        int x = Mathf.RoundToInt(vector.x);
        int y = Mathf.RoundToInt(vector.y);
        int directionIndex = 2 * x * x + x + y * y + y;
        Debug.Log(directionIndex);
        if (playerController.LastInputVector != Vector3.zero)
        {
            ChangeWalkImage(directionIndex);
        }
        if (vector == Vector2.zero)
        {
            ChangeIdleImage(_lastDirection);
            _timer = 0;
        }
        else
        {
            if (_timer == 0)
            {
                ChangeWalkImage(directionIndex);
            }
            if (timeClockInterval < _timer)
            {
                _timer -= timeClockInterval;
                _walkClock++;
                ChangeWalkImage(directionIndex);
            }
            _timer += Time.deltaTime;
            Debug.Log(directionIndex);
            _lastDirection = directionIndex;
        }
    }
    
    void ChangeWalkImage(int directionIndex)
    {
        spriteRenderer.sprite = _characterWalkSprites[_walkClock % 2][directionIndex];
    }
    
    void ChangeIdleImage(int directionIndex)
    {
        Debug.Log(directionIndex);
        spriteRenderer.sprite = characterIdleSprites[directionIndex];
    }
}

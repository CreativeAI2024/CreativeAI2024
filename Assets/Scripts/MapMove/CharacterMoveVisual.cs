using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMoveVisual : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] characterIdleSprites;
    [SerializeField] private Sprite[] characterWalkSprites;
    [SerializeField] private Sprite[] characterWalkSprites1;
    [SerializeField] private float timeClockInterval = 0.2f;
    private Sprite[][] _characterWalkSprites;
    private int _walkClock = 0;
    private float _timer = 0;
    int _lastDirection = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterWalkSprites = new Sprite[4][];
        _characterWalkSprites[0] = characterWalkSprites;
        _characterWalkSprites[1] = characterIdleSprites;
        _characterWalkSprites[2] = characterWalkSprites1;
        _characterWalkSprites[3] = characterIdleSprites;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = playerController.Direction;
        int directionIndex = GetDirectionIndex(direction);
        if (playerController.LastInputVector == Vector2.zero)
        {
            ChangeIdleImage(_lastDirection);
            _timer = 0;
        }
        else
        {
            if (_timer == 0)
            {
                ChangeWalkingImage(directionIndex);
            }
            if (timeClockInterval < _timer)
            {
                _timer -= timeClockInterval;
                _walkClock++;
                ChangeWalkingImage(directionIndex);
            }
            _timer += Time.deltaTime;
            _lastDirection = directionIndex;
        }
    }
    
    private int GetDirectionIndex(Vector2 direction)
    {
        int x = Mathf.RoundToInt(direction.x);
        int y = Mathf.RoundToInt(direction.y);
        return 2 * x * x + x + y * y + y;
    }
    
    private void ChangeWalkingImage(int directionIndex)
    {
        spriteRenderer.sprite = _characterWalkSprites[_walkClock % _characterWalkSprites.Length][directionIndex];
    }
    
    private void ChangeIdleImage(int directionIndex)
    {
        spriteRenderer.sprite = characterIdleSprites[directionIndex];
    }
}

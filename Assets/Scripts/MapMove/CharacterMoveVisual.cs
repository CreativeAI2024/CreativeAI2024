using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMoveVisual : CharacterVisual
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float timeClockInterval = 0.2f;
    private int _walkClock = 0;
    private float _timer = 0;
    int _lastDirection = 0;
    

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
                ChangeWalkingImage(directionIndex, _walkClock % GetWalkSpritesLength());
            }
            if (timeClockInterval < _timer)
            {
                _timer -= timeClockInterval;
                _walkClock++;
                ChangeWalkingImage(directionIndex, _walkClock % GetWalkSpritesLength());
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
}

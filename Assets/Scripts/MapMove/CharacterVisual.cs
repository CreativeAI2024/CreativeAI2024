using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] characterIdleSprites;
    [SerializeField] private Sprite[] characterWalkSprites;
    [SerializeField] private Sprite[] characterWalkSprites1;
    private Sprite[][] _characterWalkSprites;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterWalkSprites = new Sprite[4][];
        _characterWalkSprites[0] = characterWalkSprites;
        _characterWalkSprites[1] = characterIdleSprites;
        _characterWalkSprites[2] = characterWalkSprites1;
        _characterWalkSprites[3] = characterIdleSprites;
    }
    
    public void ChangeWalkingImage(int directionIndex, int walkIndex)
    {
        spriteRenderer.sprite = _characterWalkSprites[walkIndex][directionIndex];
    }
    
    public void ChangeIdleImage(int directionIndex)
    {
        spriteRenderer.sprite = characterIdleSprites[directionIndex];
    }
    
    protected int GetWalkSpritesLength()
    {
        return _characterWalkSprites.Length;
    }
}

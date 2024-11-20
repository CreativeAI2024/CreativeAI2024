using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerControllerWithCollider : PlayerController
{
    private BoxCollider2D _boxCollider;
    private void Start()
    {
        OnStart();
    }

    protected override void OnStart()
    {
        base.OnStart();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected override void Move()
    {
        base.Move();
        _boxCollider.offset = (Vector2)LastInputVector * (Time.deltaTime * speed);
    }
    
    protected override void MovePrepare()
    {
        base.MovePrepare();
        _boxCollider.offset = Vector2.zero;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        ResetPosition();
    }
}

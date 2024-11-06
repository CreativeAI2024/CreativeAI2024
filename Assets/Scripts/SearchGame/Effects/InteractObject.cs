using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private InputSetting _inputSetting;
    private IEffectable effect;
    private bool isFocused = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PolygonCollider2D polygonCollider2D;
    private readonly List<Vector2> points = new();
    void Start()
    {
        _inputSetting = InputSetting.Load();
        effect = GetComponent<IEffectable>();
        spriteRenderer.sprite.GetPhysicsShape(0, points);
        polygonCollider2D.points = points.ToArray();
    }
    void Update()
    {
        if (!isFocused) return;
        if (_inputSetting.GetDecideKeyDown() || Input.GetMouseButtonDown(0))
        {
            effect.PlayEffect();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        isFocused = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isFocused = false;
    }
}
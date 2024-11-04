using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class SearchGameCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private Renderer searchGame;
    [SerializeField] private Transform cursor;
    [SerializeField] private SpriteRenderer cursorImage;
    [SerializeField] private Sprite arrowCursor;
    [SerializeField] private Sprite handCursor;
    [SerializeField] private Transform cursorTip;
    [SerializeField] private float speed = 3.0f;
    private bool isFocused = false;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    void Update()
    {

        if (_inputSetting.GetForwardKey() && !IsOnUpEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.up;
        }
        if (_inputSetting.GetBackKey() && !IsOnDownEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.down;
        }
        if (_inputSetting.GetLeftKey() && !IsOnLeftEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.left;
        }
        if (_inputSetting.GetRightKey() && !IsOnRightEdge())
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
        }
    }
    public void SetIsFocusing(bool isFocusing)
    {
        this.isFocused = isFocusing;
        cursorImage.sprite = isFocusing ? handCursor : arrowCursor;
    }
    private bool IsOnUpEdge()
    {
        return cursorTip.position.y > searchGame.bounds.max.y;
    }
    private bool IsOnDownEdge()
    {
        return cursorTip.position.y < searchGame.bounds.min.y;
    }
    private bool IsOnLeftEdge()
    {
        return cursorTip.position.x < searchGame.bounds.min.x;
    }
    private bool IsOnRightEdge()
    {
        return cursorTip.position.x > searchGame.bounds.max.x;
    }
}

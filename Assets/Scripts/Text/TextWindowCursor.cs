using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindowCursor : MonoBehaviour
{
    private InputSetting _inputSetting;
    private int cursorPlace;
    private GameObject[] gameObjects;
    [SerializeField] private GameObject cursorObject;
    [SerializeField] private int cursorOffset;

    private void Start()
    {
        _inputSetting = InputSetting.Load();
        cursorPlace = 0;
    }
    
    public void CursorMove(int max)
    {
        if (cursorPlace >= 0 && cursorPlace < max)
        {
            if (_inputSetting.GetBackKeyUp() || _inputSetting.GetRightKeyUp())  //一直線に並べることを想定
            {
                cursorPlace = Mathf.Min(cursorPlace + 1, max - 1);
            }
            else if (_inputSetting.GetForwardKeyUp() || _inputSetting.GetLeftKeyUp())
            {
                cursorPlace = Mathf.Max(cursorPlace - 1, 0);
            }
            Vector2 cursorPosition = new Vector2(gameObjects[cursorPlace].transform.position.x - cursorOffset, 
                gameObjects[cursorPlace].transform.position.y);
             
            cursorObject.transform.position = cursorPosition;
        }
    }

    public void EnableCursor()
    {
        cursorPlace = 0;
        cursorObject.SetActive(true);
    }

    public void DisableCursor()
    {
        cursorObject.SetActive(false);
    }

    public void SetGameObject(GameObject[] setGameObjects)
    {
        gameObjects = setGameObjects;
    }

    public int GetCursorPlace()
    {
        return cursorPlace;
    }
}
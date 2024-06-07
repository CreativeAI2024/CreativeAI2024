using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButtonSelector : MonoBehaviour
{//カーソルはカーソルオブジェクトを作る

    private List<Button> buttons;
    private Button focusedButton;
    private int gridColumnCount;
    void Start()
    {
        buttons = new List<Button>();

        foreach(Transform child in transform)
        {
            Button buttonTmp = child.GetComponent<Button>();
            if(buttonTmp!=null)
            {
                buttons.Add(buttonTmp);
            }
            else {
                Debug.Log("There are no buttons in "+ gameObject);
            }
        }
        focusedButton = buttons[0];
        gridColumnCount = GetComponent<GridLayoutGroup>().constraintCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int focusedButtonIndex;
            if (buttons.IndexOf(focusedButton) < gridColumnCount)
            {
                focusedButtonIndex = buttons.Count;
            }
            // focusedButton = buttons[focusedButtonIndex-];
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {

        }
    }

    private void SelectButton()
    {

    }
}

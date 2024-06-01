using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetChecker : MonoBehaviour
{
    GameObject checkedButton;
    public void UseSetCursor() {
        if (checkedButton==null)
        {
            checkedButton = SearchButton();
        }
        //checkedButton.SetCursor();
    }

    private GameObject SearchButton() {
        GameObject tmp = GameObject.Find("CheckedButton");
        if (tmp==null) Debug.Log("Find CheckedButton failed");
        else Debug.Log("FInd CheckedButton successed");
        return tmp;
    }
}

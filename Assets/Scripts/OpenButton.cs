using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenButton : BaseButton
{
    GameObject parentWindow;
    Transform childWindow;
    [SerializeField] Button button; 

    void Start() 
    {
        button.GetComponent<Button>();
        button.onClick.AddListener(OpenWindow);
    }
    public void OpenWindow() {
        FindParentWindow();
        childWindow = parentWindow.transform.Find("ChildWindow");
        childWindow.gameObject.SetActive(true);
        //parentWindow.SetActive(false);
    }

    private void FindParentWindow()
    {
        parentWindow = GameObject.Find("ParentWindow");
        Debug.Log("FindParentWindow " + parentWindow!=null ?  "successed" : "failed");
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour, IEventSystemHandler
{
    [SerializeField] Button childFirstButton;
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    void OnEnable()
    {
    }

}

// OpenButton.csの内容
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class OpenButton : BaseButton
// {
//     GameObject parentWindow;
//     Transform childWindow;
//     [SerializeField] Button button; 

//     void Start() 
//     {
//         button.GetComponent<Button>();
//         button.onClick.AddListener(OpenWindow);
//     }
//     public void OpenWindow() {
//         FindParentWindow();
//         childWindow = parentWindow.transform.Find("ChildWindow");
//         childWindow.gameObject.SetActive(true);
//         //parentWindow.SetActive(false);
//     }

//     private void FindParentWindow()
//     {
//         parentWindow = GameObject.Find("ParentWindow");
//         Debug.Log("FindParentWindow " + parentWindow!=null ?  "successed" : "failed");
//     }
// }

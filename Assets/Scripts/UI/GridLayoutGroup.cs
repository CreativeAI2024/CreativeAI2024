using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridLayoutGroup : MonoBehaviour
{
    private List<Button> buttons;
    [SerializeField] Button headButton;
    void Start()
    {
        buttons = new List<Button>();

        foreach (Transform child in transform)
        {
            Button buttonTmp = child.GetComponent<Button>();
            if (buttonTmp != null)
            {
                buttons.Add(buttonTmp);
            }
            else
            {
                Debug.Log("There are no buttons in " + gameObject);
            }
        }
    }

    void Update()
    {

    }

    void OnEnable()
    {
        StartCoroutine("focusHeadButton");
    }

    private void focusHeadButton()
    {
        EventSystem.current.SetSelectedGameObject(headButton.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridLayoutGroup : MonoBehaviour
{
    [SerializeField] Button headButton;
    void OnEnable()
    {
        focusHeadButton();
    }

    private void focusHeadButton()
    {
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(headButton.gameObject);
        }
    }
}

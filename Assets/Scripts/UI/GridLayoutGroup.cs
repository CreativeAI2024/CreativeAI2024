using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridLayoutGroup : MonoBehaviour
{
    [SerializeField] Button headButton;
    void OnEnable()
    {
        if (headButton==null)
        {
            Debug.Log("SerializeField 'headButton' of " + transform.parent.gameObject + "'s grid is null.");
        }
        focusHeadButton();
    }
    private void focusHeadButton()
    {
        if(!IsCurrentEventSystemNull())
        {
            EventSystem.current.SetSelectedGameObject(headButton.gameObject);
        }
    }
    private bool IsCurrentEventSystemNull()
    {
        return EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null;
    }
}

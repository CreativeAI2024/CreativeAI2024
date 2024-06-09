using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIUtility : MonoBehaviour
{
    public static bool IsCurrentEventSystemNull()
    {
        return EventSystem.current == null || EventSystem.current.currentSelectedGameObject == null;
    }
    public static void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

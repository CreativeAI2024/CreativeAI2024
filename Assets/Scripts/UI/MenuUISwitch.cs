using Unity.VisualScripting;
using UnityEngine;

public class MenuUISwitch : MonoBehaviour
{
    [SerializeField] private GameObject menuUIWindows;
    
    void OnEnable()
    {
        menuUIWindows.SetActive(false);
    }

    void OnDisable()
    {
        menuUIWindows.SetActive(true);
    }
}
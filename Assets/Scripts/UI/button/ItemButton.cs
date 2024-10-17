using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetButtonName(string buttonName)
    {
        itemName.text = buttonName;
    }
}

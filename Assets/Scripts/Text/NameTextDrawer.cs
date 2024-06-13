using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTextDrawer : MonoBehaviour
{
    private TextMeshProUGUI _nameTextObject;
    private GameObject _nameTextPanel;

     void Start()
    {
        _nameTextObject = GetComponent<TextMeshProUGUI>();
        _nameTextPanel = transform.parent.gameObject;
    }

    public void NamePanelSwitch()
    {
        _nameTextPanel.SetActive(false);
    }

    public void NameText(string str)
    {
        //–¼‘O‚ð•\Ž¦‚·‚é
        _nameTextPanel.SetActive(true);
        _nameTextObject.text = str;
    }
}

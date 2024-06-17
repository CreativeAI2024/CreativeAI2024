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

    public void DisplayNameText(string[] words)
    {
        if (words.Length > 1)
        {
            NameText(words[0]);
        }
        else
        {
            NamePanelSwitch();
        }
    }

    private void NamePanelSwitch()
    {
        _nameTextPanel.SetActive(false);
    }

    private void NameText(string str)
    {
        //–¼‘O‚ð•\Ž¦‚·‚é
        _nameTextPanel.SetActive(true);
        _nameTextObject.text = str;
    }
}

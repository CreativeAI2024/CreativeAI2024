using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NameTextDrawer : MonoBehaviour
{
    private TextMeshProUGUI _nameTextObject;
    private GameObject _nameTextPanel;

    public void Initialize()
    {
        _nameTextObject = GetComponent<TextMeshProUGUI>();
        _nameTextPanel = transform.parent.gameObject;
    }

    public void DisplayNameText(string words)
    {
        NameText(words);
    }

    public void DisableNameText()
    {
        _nameTextPanel.SetActive(false);
    }

    private void NameText(string str)
    {
        _nameTextPanel.SetActive(true);
        _nameTextObject.text = str;
    }
}

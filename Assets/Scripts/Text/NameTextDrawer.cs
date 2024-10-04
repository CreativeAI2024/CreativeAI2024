using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;


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

    public void DisplayNameText(string[] words)
    {
        for (int i = 0; i < words.Length; i++)
        {
            if (!words[i].StartsWith("[speaker]")) continue; //[speaker]タグを探す

            string word = words[i].Split(']')[1];  //タグを外す
            NameText(word);
            return;

        }
        _nameTextPanel.SetActive(false);
    }

    private void NameText(string str)
    {
        _nameTextPanel.SetActive(true);
        _nameTextObject.text = str;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTextDrawer : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI _nameTextObject;
    public GameObject _nameTextPrefab;

    private void Start()
    {
        _nameTextObject = GetComponent<TextMeshProUGUI>();
    }
    public void NameText(string str)
    {
        //–¼‘O‚ð•\Ž¦‚·‚é
        _nameTextPrefab.SetActive(true);
        _nameTextObject.text = str;
    }
}

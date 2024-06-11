using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTextDrawer : MonoBehaviour
{

    public TextMeshProUGUI _nameTextObject;
    public GameObject _nameTextPrefab;

    public void NameText(string str)
    {
        //–¼‘O‚ð•\Ž¦‚·‚é
        _nameTextPrefab.SetActive(true);
        _nameTextObject.text = str;
    }
}

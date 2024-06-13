using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubyDrawer : MonoBehaviour
{
    [HideInInspector] public TextMeshProRuby rb;

    private void Start()
    {
        rb = GetComponent<TextMeshProRuby>();
    }
    public void RubySpawner(string str)
    {
        rb.Text = str;
    }
}

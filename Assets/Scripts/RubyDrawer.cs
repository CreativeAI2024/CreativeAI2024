using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RubyDrawer : MonoBehaviour
{
    public TextMeshProRuby rb;

    public void RubySpawner(string str)
    {
        rb.Text = str;
    }
}

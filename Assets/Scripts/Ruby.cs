using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby : MonoBehaviour
{
    [SerializeField] GameObject _mainTextObject;

    // Start is called before the first frame update
    void Start()
    {
        _mainTextObject.AddComponent<TextMeshProRuby>();
        //_mainTextObject.AddComponent<TMProRubyUtil>();
    }

    // Update is called once per frame
}

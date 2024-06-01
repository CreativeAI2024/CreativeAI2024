using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method called.");
        this.UnsetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCursor() {
        this.ChangeCursorState(true);
    }

    public void UnsetCursor() {
        this.ChangeCursorState(false);
    }

    private void ChangeCursorState(bool b) {
        Debug.Log("ChangeCursorState method called with b=" + b);
        this.GetComponent<Image>().enabled = b;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public static Cursor cursor = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (cursor==null)
        {
            cursor = this;
        }
    }
    void Start()
    {
        setPosition();
    }

    // Update is called once per frame
    void Update()
    {
        changePosition();
    }

    void changePosition() 
    {
        if (transform.position != EventSystem.current.currentSelectedGameObject.transform.position)
        {
            setPosition();
        }
    }
    private void setPosition()
    {
        transform.position = EventSystem.current.currentSelectedGameObject.transform.position;
    }
}

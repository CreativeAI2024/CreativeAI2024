using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorTip : MonoBehaviour
{
    [SerializeField] private Renderer cursorTip;
    [SerializeField] private SearchGameCursor cursor;
    [SerializeField] private SearchGameManager searchGameManager;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (searchGameManager.EventTriggerImageRenderers.Any(x =>
        {
            Debug.Log("image: "+x);
            Debug.Log("cursorTip: "+cursorTip);
            return x.bounds.Intersects(cursorTip.bounds);
        }))
        {
            cursor.SetIsFocused(true);
        }
        else
        {
            cursor.SetIsFocused(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collide: "+other.gameObject.name);
    }
}

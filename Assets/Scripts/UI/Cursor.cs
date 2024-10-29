using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    public void Focus(Vector3 destination)
    {
        transform.position = destination;
    }
}

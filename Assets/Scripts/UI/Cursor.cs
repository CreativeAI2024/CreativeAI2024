using UnityEngine;

public class Cursor : MonoBehaviour
{
    public void Focus(Vector3 destination)
    {
        transform.position = destination;
    }
}

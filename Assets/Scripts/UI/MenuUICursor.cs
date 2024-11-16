using UnityEngine;

public class MenuUICursor : MonoBehaviour
{
    public void Focus(Vector3 destination)
    {
        transform.position = destination;
    }
}

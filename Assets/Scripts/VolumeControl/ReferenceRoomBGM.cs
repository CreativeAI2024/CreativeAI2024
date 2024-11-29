using UnityEngine;

public class ReferenceRoomBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(5, 1f);
    }
}

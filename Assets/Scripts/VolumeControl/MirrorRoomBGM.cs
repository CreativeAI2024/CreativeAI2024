using UnityEngine;

public class MirrorRoomBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(3, 1f);
    }
}

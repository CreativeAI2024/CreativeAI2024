using UnityEngine;

public class TessenRoomBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(5, 1f);
    }
}

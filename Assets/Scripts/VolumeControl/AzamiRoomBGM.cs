using UnityEngine;

public class AzamiRoomBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(4, 1f);
    }
}

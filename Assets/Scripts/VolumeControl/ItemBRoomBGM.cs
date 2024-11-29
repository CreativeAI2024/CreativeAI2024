using UnityEngine;

public class ItemBrRoomBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(1, 1f);
    }
}

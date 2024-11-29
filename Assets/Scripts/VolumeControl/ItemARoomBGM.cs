using UnityEngine;

public class ItemARoomBGM : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(2, 1f);
    }
}

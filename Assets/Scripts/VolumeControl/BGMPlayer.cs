using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public int bgmID;

    void Start()
    {
        SoundManager.Instance.PlayBGM(bgmID, 1f);
    }
}

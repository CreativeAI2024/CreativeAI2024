using UnityEngine;
using UnityEngine.SceneManagement;  // シーン情報を取得するために必要

public class BGMManager : MonoBehaviour
{
    void Start()
    {
        // 現在のシーン名を取得
        string sceneName = SceneManager.GetActiveScene().name;

        // シーン名に応じたBGMを再生
        int bgmID = GetBGMIDForScene(sceneName);
        SoundManager.Instance.PlayBGM(bgmID, 1f);  // BGMを再生
    }

    // シーン名に応じてBGMのIDを返すメソッド
    private int GetBGMIDForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "reference_room":
                return 1;
            case "tessen_room":
                return 2;
            case "mirror_room":
                return 3;
            case "azami_room":
                return 4;
            case "itemA_room":
                return 5;
            case "itemB_room":
                return 6;
            default:
                return 0;
        }
    }
}

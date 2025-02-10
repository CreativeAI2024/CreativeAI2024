using UnityEngine;
using UnityEngine.SceneManagement;

public class Re_enteredAzamiRoom : CharatipDisplay
{
    protected override void Update()
    {
    if (charatip.enabled && FlagManager.Instance.HasFlag("FirstLeaveFromAzamiRoom"))
        {
            DebugLogger.Log($"HikaruMeetToAzami Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
        }
    }
    // Actionに関数をAddしても、破棄されたシーン内の関数は実行しようとしてもSpriteRendererなどが失われてしまっていた。
    // DontDestroyのCharatipDisplayManagerを作り、そこにGameObjectごとListにAddしても、シーンが変わるとアクセスできなかった。
    // 仕方がないので、Updateで行う
}
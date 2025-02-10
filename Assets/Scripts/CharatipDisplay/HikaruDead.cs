using UnityEngine;
using UnityEngine.SceneManagement;

public class HikaruDead : CharatipDisplay
{
    protected override void Update()
    {
        // 普段の立ち絵、死体、血溜まりのSpriteを持っておき、条件に合わせて複数のSpriteRendererを操作する。
        if (charatip.enabled && FlagManager.Instance.HasFlag("FirstLeaveFromAzamiRoom"))
        {

        }
    }
}
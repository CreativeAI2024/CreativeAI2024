public class HikaruBlood : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        // 普段の立ち絵、死体、血溜まりのSpriteを持っておき、条件に合わせて複数のSpriteRendererを操作する。
        if (!charatip.enabled && FlagManager.Instance.HasFlag("Hikaru_Dead"))
        {
            DebugLogger.Log($"HikaruBlood Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
    }
}
public class HikaruDead : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        // 普段の立ち絵、死体、血溜まりのSpriteを持っておき、条件に合わせて複数のSpriteRendererを操作する。
        if (!charatip.enabled && FlagManager.Instance.HasFlag("Hikaru_Dead") && !FlagManager.Instance.HasFlag("Hikaru_Eaten"))
        {
            DebugLogger.Log($"HikaruDead Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
        if (charatip.enabled && FlagManager.Instance.HasFlag("Hikaru_Eaten"))
        {
            DebugLogger.Log($"HikaruDead Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
        }
    }
}
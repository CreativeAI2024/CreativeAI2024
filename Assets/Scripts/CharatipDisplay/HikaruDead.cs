public class HikaruDead : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
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
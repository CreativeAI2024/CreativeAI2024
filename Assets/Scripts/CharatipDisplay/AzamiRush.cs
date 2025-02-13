using UnityEngine;

public class AzamiRush : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        if (!charatip.enabled && FlagManager.Instance.HasFlag("AzamiRush"))
        {
            DebugLogger.Log($"AzamiRush Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
        if (charatip.enabled && !FlagManager.Instance.HasFlag("AzamiRush"))
        {
            DebugLogger.Log($"AzamiRush Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
        }
    }
}
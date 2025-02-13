using UnityEngine;
public class AzamiIntoMirror : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        FlagManager flag = FlagManager.Instance;
        if (!charatip.enabled && flag.HasFlag("Progress10"))
        {
            DebugLogger.Log($"AzamiIntoMirror Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
    }
}
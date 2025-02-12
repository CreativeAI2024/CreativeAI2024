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

        if (!charatip.enabled && FlagManager.Instance.HasFlag("Progress10"))
        {
            DebugLogger.Log($"AzamiRush Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
            Vector3 newPosition = new Vector3(5,1.5f,0);
            charatip.transform.position = newPosition;
        }
    }
}
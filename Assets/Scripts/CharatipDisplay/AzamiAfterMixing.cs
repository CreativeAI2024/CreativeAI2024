using UnityEngine;
public class AzamiAfterMixing : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        if (!charatip.enabled && (FlagManager.Instance.HasFlag("ClearMixing1") || FlagManager.Instance.HasFlag("Progress8")))
        {
            DebugLogger.Log($"AzamiAfterMixing1 Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
        if (charatip.enabled && FlagManager.Instance.HasFlag("LeaveReferenceRoomAfterMixing1"))
        {
            DebugLogger.Log($"HikaruMeetToAzami Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
            FlagManager.Instance.DeleteFlag("LeaveReferenceRoomAfterMixing1");
        }
    }
}
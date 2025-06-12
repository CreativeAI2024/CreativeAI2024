using UnityEngine;
public class AzamiAfterMixing : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        FlagManager flag = FlagManager.Instance;
        if (!charatip.enabled && ((flag.HasFlag("ClearMixing1") && !flag.HasFlag("LeaveReferenceRoomAfterMixing1"))) || flag.HasFlag("Progress8"))
        {
            DebugLogger.Log($"AzamiAfterMixing1 Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
        if (charatip.enabled && flag.HasFlag("LeaveReferenceRoomAfterMixing1"))
        {
            DebugLogger.Log($"HikaruMeetToAzami Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
            flag.DeleteFlag("LeaveReferenceRoomAfterMixing1");
        }
    }
}
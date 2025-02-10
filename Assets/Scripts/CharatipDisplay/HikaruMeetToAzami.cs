using UnityEngine;

public class HikaruMeetToAzami : CharatipDisplay
{
    protected override void ChangeCharatipVisibility()
    {
    if (FlagManager.Instance.HasFlag("HasLeftAzamiRoom"))
        {
            DebugLogger.Log($"HikaruMeetToAzami Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
        }
    }
}
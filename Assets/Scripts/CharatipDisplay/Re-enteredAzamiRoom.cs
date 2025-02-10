using UnityEngine;

public class Re_enteredAzamiRoom : CharatipDisplay
{
    protected override void ChangeCharatipVisibility()
    {
    if (FlagManager.Instance.HasFlag("Hikaru_Back_To_Mirror_Room"))
        {
            DebugLogger.Log($"HikaruMeetToAzami Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
            FlagManager.Instance.DeleteFlag("Hikaru_Back_To_Mirror_Room");
        }
    }
}
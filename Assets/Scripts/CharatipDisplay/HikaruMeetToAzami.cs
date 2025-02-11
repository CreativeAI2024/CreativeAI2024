public class HikaruMeetToAzami : CharatipDisplay
{
    void Start()
    {
        ChangeCharatipVisibility();
    }

    public override void ChangeCharatipVisibility()
    {
        if (charatip.enabled && FlagManager.Instance.HasFlag("FirstLeaveFromAzamiRoom"))
        {
            DebugLogger.Log($"HikaruMeetToAzami Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
        }
    }
}
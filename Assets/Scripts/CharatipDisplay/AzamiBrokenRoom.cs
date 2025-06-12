using UnityEngine;
using UnityEngine.SceneManagement;

public class AzamiBrokenRoom : CharatipDisplay
{
    public override void ChangeCharatipVisibility()
    {
        if (!charatip.enabled && FlagManager.Instance.HasFlag("BrokenRoom"))
        {
            Vector3 newPosition = new Vector3(0, 0, 0);
            if (SceneManager.GetActiveScene().name.Equals("itemA_room_broken"))
            {
                newPosition += new Vector3(5, 2.5f, 0);
            }else if (SceneManager.GetActiveScene().name.Equals("itemB_room_broken"))
            {
                newPosition += new Vector3(3, 2.5f, 0);
            }
            charatip.transform.position = newPosition;
            DebugLogger.Log($"AzamiRush Displayed.", DebugLogger.Colors.Yellow);
            charatip.enabled = true;
        }
        if (charatip.enabled && !FlagManager.Instance.HasFlag("BrokenRoom"))
        {
            DebugLogger.Log($"AzamiRush Hidden.", DebugLogger.Colors.Yellow);
            charatip.enabled = false;
        }
    }
}
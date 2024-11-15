using UnityEngine;

public class BrokenTeddyBearEffect : MonoBehaviour, IEffectable
{
    public void PlayEffect()
    {
        ConversationTextManager.Instance.InitializeFromString("There are something in the stomach of teddy bear. <br>You got a soul of Teddy Bear.");
        DebugLogger.Log("Pause. ");
    }
}

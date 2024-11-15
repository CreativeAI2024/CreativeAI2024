using UnityEngine;

public class ZombieEffect : MonoBehaviour, IEffectable
{
    public void PlayEffect()
    {
        ConversationTextManager.Instance.InitializeFromString("Henji ga Nai. <br>Tadano Sikabane no Youda.");
        DebugLogger.Log("Pause. ");
    }
}

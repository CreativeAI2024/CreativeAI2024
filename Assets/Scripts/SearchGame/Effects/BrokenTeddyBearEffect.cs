using UnityEngine;

public class BrokenTeddyBearEffect : MonoBehaviour, IEffectable
{
    [SerializeField] private Pause pause;
    public void PlayEffect()
    {
        pause.PauseAll();
        Debug.Log("テディベアの破れたお腹の中に何かある。");
        Debug.Log("テディベアの心を手に入れた。");
    }
}

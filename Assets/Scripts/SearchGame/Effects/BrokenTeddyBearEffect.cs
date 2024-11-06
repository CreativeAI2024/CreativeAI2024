using UnityEngine;

public class BrokenTeddyBearEffect : MonoBehaviour, IEffectable
{
    public void PlayEffect()
    {
        Debug.Log("テディベアの破れたお腹の中に何かある。");
        Debug.Log("テディベアの心を手に入れた。");
    }
}

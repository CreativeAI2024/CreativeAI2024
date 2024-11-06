using UnityEngine;

public class BrokenTeddyBearEffect : MonoBehaviour, IEffect
{
    public void Run()
    {
        Debug.Log("テディベアの破れたお腹の中に何かある。");
        Debug.Log("テディベアの心を手に入れた。");
    }
}

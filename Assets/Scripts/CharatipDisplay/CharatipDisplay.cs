using UnityEngine;

// 各シーンに表示したいキャラチップをアタッチしたオブジェクトを追加しておき、必要なタイミングで表示, 非表示を切り替える
public class CharatipDisplay : MonoBehaviour
{
    void Start()
    {
        CharatipDisplayManager.Instance.RegisterCharatipDisplay(this);
        gameObject.SetActive(CharatipDisplayManager.Instance.GetIsVisible(name));
    }
    public void Display()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
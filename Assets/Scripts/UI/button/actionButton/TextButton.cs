using UnityEngine;

public class TextButton : ItemActionButton
{
    [SerializeField] private GameObject conversationWindow;
    //会話ウィンドウはInstantiate()か、SetActive(true)で起動するか未確定。TextButtonは暫定的。
    protected override void OnStart(){}
    public override void OnDecideKeyDown()
    {
        //会話ウィンドウにテキストを渡す処理。渡し方未確定。    
    }
    public override void OnCancelKeyDown()
    {
        //ItemWindowに戻る処理
    }
}
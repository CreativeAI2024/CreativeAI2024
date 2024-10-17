using UnityEngine;

public class ImageTextButton : ItemActionButton
{
    [SerializeField] private GameObject conversationWindow;
    //会話ウィンドウはInstantiate()か、SetActive(true)で起動するか未確定。TextButtonは暫定的。
    protected override void OnStart()
    {
        openWindow.NextWindow = conversationWindow;
    }

    public override void OnDecideKeyDown(){
        //会話ウィンドウに画像を渡す処理。渡し方未確定。
        //会話ウィンドウにテキストを渡す処理。渡し方未確定。
    }

}
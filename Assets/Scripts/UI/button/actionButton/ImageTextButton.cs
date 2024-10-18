using UnityEngine;

public class ImageTextButton : ItemActionButton
{
    [SerializeField] private TextButton textButton;
    [SerializeField] private GameObject conversationWindow;
    //会話ウィンドウはInstantiate()か、SetActive(true)で起動するか未確定。TextButtonは暫定的。
    protected override void OnStart()
    {
    }

    public override void OnDecideKeyDown()
    {
        //会話ウィンドウに画像を渡す処理。渡し方未確定。
        textButton.OnDecideKeyDown();
    }
    public override void OnCancelKeyDown()
    {
        //ItemWindowに戻る
    }
}
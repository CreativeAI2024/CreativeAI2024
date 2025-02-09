using UnityEngine;

public class CharatipDisplay : DontDestroySingleton<CharatipDisplay>
{
    //使うSpriteとその座標をSerializeFieldで取得
    //会話ウィンドウに呼び出してもらう、Display()とHide()を作る
    [SerializeField] private Sprite hikaruBack;
    [SerializeField] private Sprite hikaruDead;
    [SerializeField] private Sprite azami;
    [SerializeField] private Sprite blood;

    [SerializeField] private Vector3 hikaruMeetToAzamiPosition;
    [SerializeField] private Vector3 hikaruDeadPosition;
    [SerializeField] private Vector3 hikaruBloodPosition;
    [SerializeField] private Vector3 azamiInReferenceRoomPosition;

    public void DisplayHikaruMeetToAzami()
    {

    }

    private void DisplaySprite(Sprite sprite)
    {
        sprite.enabled = true;
    }

    private void HideSprite(Sprite sprite)
    {
        sprite.enabled = false;
    }
}
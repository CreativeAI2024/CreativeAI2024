using UnityEngine;
using UnityEngine.UI;

public class ImageButton : ItemActionButton
{
    [SerializeField] private ItemImageWindow itemImageWindow;
    [SerializeField] private Image screenImage;
    protected override void OnStart(){}
    public override void OnDecideKeyDown()
    {
        //ItemImageWindowを開き、screenImageを表示する処理
    }
    public override void OnCancelKeyDown()
    {
        //ItemWindowに戻る処理
    }
}
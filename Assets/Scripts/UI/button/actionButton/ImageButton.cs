using UnityEngine;
using UnityEngine.UI;

public class ImageButton : ItemActionButton
{
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private Image screenImage;
    protected override void OnStart(){}
    public override void OnDecideKeyDown()
    {
        openWindow.NextWindow = itemImageScreen;
        screenImage.sprite = thisItem.Image;
    }
}
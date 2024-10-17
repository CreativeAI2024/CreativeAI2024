using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//TODO: confirmWindowでyes推したのに合成できていない
//TODO: アイテムの機能作る
//TODO:     テキスト表示機能
//TODO: スクロール機能
//TODO: 改行に合わせて説明ウィンドウの縦幅を増やす機能
//TODO: 確認ウィンドウに合成アイテム名を表示
//TODO: アイテム合成の時に、アイテムの個数のことを考えずに問答無用でDestroyしていた
//TODO: ItemButtonに個数表示機能
//TODO: スクリプト実行順見直す
//TODO: デバッグ
//TODO: メニューUIの構造の見直し（自分以外でも使えるように）
//TODO:     プレハブを実体化すればすぐメニューUIが使えるようにする
//カーソル移動ロック機能作る→今のところ不要になった
//アイテム画像は1600*900で出してもらうことを視覚班に伝える→ゲーム全体の解像度によるので保留
//TODO: アイテム周りのクラス図を作る
//ImageShowItem Objectは画像と、そのgetterを持っている
//ItemWindowはButtons内のItemにfor文で全てにアクセスできる
//引数の型がItemでも、実際に引数として渡される変数がそれを継承したImageShowItemクラスなら、ImageShowItemクラスとして認識される

public class ItemWindowButtons : MonoBehaviour
{
    [SerializeField] private ItemInventory itemInventory;
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private GameObject itemButtonPrefab;
    [SerializeField] private GameObject actionsWindow;
    private Dictionary<Item, GameObject> itemButtonDict = new Dictionary<Item, GameObject>();
    void OnEnable()
    {
        LoadItemInventory();
    }

    private void LoadItemInventory()
    {
        foreach (KeyValuePair<Item, GameObject> item in itemButtonDict)
        {
            if (!itemInventory.GetItems().Contains(item.Key))
            {
                Destroy(item.Value);
            }
        }
        foreach (Item item in itemInventory.GetItems())
        {
            if (!itemButtonDict.ContainsKey(item))
            {
                GameObject itemButton = MakeItemButton(item);
                itemButtonDict.Add(item, itemButton);
            }
        }
    }

    private GameObject MakeItemButton(Item item)
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, transform);
        itemButton.GetComponent<ItemButton>().SetButtonName(item.ItemName);
        OpenWindow openWindow = itemButton.GetComponent<OpenWindow>();
        openWindow.Initialize(transform.parent.gameObject, actionsWindow);
        return itemButton;
    }
}
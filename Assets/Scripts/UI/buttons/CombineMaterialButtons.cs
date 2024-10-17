// using System;
// using System.Collections.Generic;
// using TMPro;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.UIElements;

// //TODO: CombineMaterialsWindowを作り、そのButtonsにこれをアタッチする
// //TODO: 現在はItemWindowButtonsをコピペしただけ
// public class CombineMaterialButtons : MonoBehaviour
// {
//     [SerializeField] private ItemInventory itemInventory;
//     [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
//     [SerializeField] private GameObject itemButtonPrefab;
//     [SerializeField] private GameObject actionsWindow;

//     void OnEnable()
//     {
//         LoadItemInventory();
//     }

//     private void LoadItemInventory()
//     {
//         foreach (Transform itemButton in transform)
//         {
//             if (!itemInventory.GetItem(itemButton.GetChild(0).GetComponent<TextMeshProUGUI>().text)) //アイテム一覧のボタンにあってアイテムリストにないボタンを確かめる
//             {
//                 Destroy(itemButton.gameObject);
//             }
//         }
//         foreach (Item item in itemInventory.GetItems())
//         {
//             if (!Search(item.ItemName))
//             {
//                 MakeItemButton(item);
//             }
//         }
//     }

//     //アイテム合成の時にリストを探索する時に呼び出す
//     //上の場合でも、ItemInventory内を探せばいいのでは？
//     private GameObject Search(string searchedButtonName)
//     {
//         foreach (Transform child in transform)
//         {
//             GameObject button = child.gameObject;
//             if (GetButtonName(button).Equals(searchedButtonName))
//             {
//                 return button;
//             }
//         }
//         return null;
//     }
//     private void MakeItemButton(Item item)
//     {
//         GameObject itemButton = Instantiate(itemButtonPrefab, transform);
//         SetButtonName(itemButton, item);
//         OpenWindow openWindow = itemButton.GetComponent<OpenWindow>();
//         openWindow.CurrentWindow = transform.parent.gameObject;
//         openWindow.NextWindow = actionsWindow;
        
//     }


//     private string GetButtonName(GameObject button)
//     {
//         return button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
//     }

//     private void SetButtonName(GameObject button, Item buttonItem)
//     {
//         button.name = buttonItem.ItemName;
//         button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonItem.ItemName;
//     }
// }
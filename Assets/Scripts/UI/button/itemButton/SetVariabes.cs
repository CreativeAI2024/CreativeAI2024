using TMPro;
using UnityEngine;

abstract public class SetVariables : MonoBehaviour
{
  protected InputSetting _inputSetting;
  protected GameObject uiManager;
  protected ItemInventory itemInventory;
  protected BaseItem thisItem;
  protected bool isOnEnableFirstRun = true;
  protected void Start()
  {
    _inputSetting = InputSetting.Load();
    uiManager = GameObject.FindWithTag("UIManager");
    itemInventory = Resources.Load<ItemInventory>("Items/ItemInventory");
    thisItem = itemInventory.GetItem(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text); //押したボタンのテキストからアイテムを取得 かなた質問：ボタンオブジェクトにアイテムボタンを保管するスクリプト作った方がいい？
  }
}
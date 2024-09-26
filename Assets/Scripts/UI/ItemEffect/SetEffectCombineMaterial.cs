using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetEffectCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemList itemList;
  private GameObject uiManager;
  private CombineMaterialItem thisItem;
  private GameObject confirmYesButton;
  void Start()
  {
    _inputSetting = InputSetting.Load();
    itemList = Resources.Load<ItemList>("Items/ItemList");
    uiManager = GameObject.FindWithTag("UIManager");
    thisItem = (CombineMaterialItem)itemList.Search(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
  }
  void Update()
  {
    if (_inputSetting.GetDecideKeyDown())
    {
      if (EventSystem.current.currentSelectedGameObject == gameObject)
      {
        if (itemList.Search(thisItem.PairItem.ItemName) == true)
        {
          Debug.Log("ChangeEnabled(true)");
          ChangeEnabled(true);
          confirmYesButton.GetComponent<Combine>().ItemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
        else{
          ChangeEnabled(false);
        }
      }
    }
  }

  private void ChangeEnabled(bool isCombinable)
  {
    transform.GetComponent<OpenWindow>().enabled = isCombinable;
  }

}
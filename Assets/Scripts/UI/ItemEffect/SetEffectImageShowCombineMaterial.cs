using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetEffectImageShowCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemList itemList;
  private GameObject uiManager;
  private CombineMaterialItem thisItem;
  private Sprite itemImage;
  private GameObject confirmWindow;
  private GameObject confirmYesButton;
  private GameObject itemImageScreen;

  void Awake()
  {
    itemList = Resources.Load<ItemList>("Items/ItemList");
    thisItem = (CombineMaterialItem)itemList.Search(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
  }
  void Start()
  {
    _inputSetting = InputSetting.Load();
    uiManager = GameObject.FindWithTag("UIManager");
    string itemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
    itemImage = ((ImageShowCombineMaterialItem)itemList.Search(itemName)).Image;
    confirmWindow = uiManager.GetComponent<GameObjectHolder>().ConfirmWindow;
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
    itemImageScreen = uiManager.GetComponent<GameObjectHolder>().ItemImageScreen;
  }

  void OnEnable()
  {
    if (itemList.Search(thisItem.PairItem.ItemName) == true)
    {
      ChangeEnabled(true);
    }
    else
    {
      ChangeEnabled(false);
    }
  }
  void Update()
  {
    if (_inputSetting.GetDecideKeyDown())
    {
      if (EventSystem.current.currentSelectedGameObject == gameObject)
      {
        itemImageScreen.GetComponent<Image>().sprite = itemImage;
        if (itemList.Search(thisItem.PairItem.ItemName) == true)
        {
          ChangeNextWindow(true);
          confirmYesButton.GetComponent<Combine>().ItemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
        else
        {
          ChangeNextWindow(false);
        }
      }
    }
  }

  private void ChangeEnabled(bool isCombinable)
  {
    transform.GetComponent<OpenWindow>().enabled = isCombinable;
  }

  private void ChangeNextWindow(bool isCombinable)
  {
    if (isCombinable)
    {
      itemImageScreen.GetComponent<OpenWindow>().nextWindow = confirmWindow;
    }
    else
    {
      itemImageScreen.GetComponent<OpenWindow>().nextWindow = transform.parent.parent.gameObject;
    }
  }

}
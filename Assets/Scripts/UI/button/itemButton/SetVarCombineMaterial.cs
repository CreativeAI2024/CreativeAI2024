using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetVarCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private ItemList itemList;
  private GameObject uiManager;
  private CombineMaterialItem thisItem;
  private GameObject confirmYesButton;
  private CSetCombine cSetCombine;
  void Awake()
  {
    itemList = Resources.Load<ItemList>("Items/ItemList");
    thisItem = (CombineMaterialItem)itemList.Search(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
    cSetCombine = new CSetCombine();
  }
  void Start()
  {
    _inputSetting = InputSetting.Load();
    uiManager = GameObject.FindWithTag("UIManager");
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
  }

  void OnEnable()
  {
    if (itemList.Search(thisItem.PairItem.ItemName) == true)
    {
      cSetCombine.SetEnabled(gameObject, true);
    }
    else
    {
      cSetCombine.SetEnabled(gameObject, false);
    }
  }
  void Update()
  {
    if (_inputSetting.GetDecideKeyDown())
    {
      if (EventSystem.current.currentSelectedGameObject == gameObject)
      {
        if (itemList.Search(thisItem.PairItem.ItemName) == true)
        {
          cSetCombine.SetItemName(confirmYesButton, thisItem.ItemName);
        }
      }
    }
  }
}
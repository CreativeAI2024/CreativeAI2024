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
  void Awake()
  {
    itemList = Resources.Load<ItemList>("Items/ItemList");
    thisItem = (CombineMaterialItem)itemList.Search(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
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
        if (itemList.Search(thisItem.PairItem.ItemName) == true)
        {
          confirmYesButton.GetComponent<Combine>().ItemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
      }
    }
  }

  private void ChangeEnabled(bool isCombinable)
  {
    transform.GetComponent<OpenWindow>().enabled = isCombinable;
  }

}
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetCombineMaterial : MonoBehaviour
{
  private InputSetting _inputSetting;
  private GameObject uiManager;
  private GameObject confirmYesButton;
  void Start()
  {
    _inputSetting = InputSetting.Load();
    uiManager = GameObject.FindWithTag("UIManager");
    confirmYesButton = uiManager.GetComponent<GameObjectHolder>().ConfirmYesButton;
  }
  void Update()
  {
    if (_inputSetting.GetDecideKeyDown())
    {
      if (EventSystem.current.currentSelectedGameObject == gameObject)
      {
        if (gameObject.GetComponent<OpenWindow>()==true)
        {
          confirmYesButton.GetComponent<Combine>().ItemName = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        }
      }
    }
  }
}
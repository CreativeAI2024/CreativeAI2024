using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayDescription : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private ItemInventory itemInventory;
    private TextMeshProUGUI textComponent;
    private Item focusedButton;
    private bool isOnEnableFirstRun = true;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        textComponent = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Set();
    }

    void OnEnable()
    {
        if (isOnEnableFirstRun)
        {
            isOnEnableFirstRun = false;
        }
        else
        {
            Set();
        }
    }
    void Update()
    {
        if (_inputSetting.GetForwardKeyDown() || _inputSetting.GetBackKeyDown() || _inputSetting.GetLeftKeyDown() || _inputSetting.GetRightKeyDown())
        {
            Set();
        }
    }
    private void Set()
    {
        focusedButton = itemInventory.GetItem(EventSystem.current.currentSelectedGameObject.name);
        textComponent.text = focusedButton.DescriptionText;
    }
}

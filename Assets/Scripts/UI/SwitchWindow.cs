using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SwitchWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    [SerializeField] private GameObject topWindow;
    [SerializeField] private GameObject ImageOfImageShowItem;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if ((_inputSetting.GetItemKeyDown() || (_inputSetting.GetCancelKeyDown()&&topWindow.activeSelf)) && ImageOfImageShowItem.GetComponent<Image>().enabled == false)
        {
            Switch();
        }
    }
    public void Switch()
    {
        ChangeActive(windowBox, !windowBox.activeSelf);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}

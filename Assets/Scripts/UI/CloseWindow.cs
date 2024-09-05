using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class CloseWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject previousWindow;
    [SerializeField] private GameObject ImageOfImageShowItem;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetCancelKeyDown() && previousWindow != null && ImageOfImageShowItem.GetComponent<Image>().enabled == false)
        {
            Close();
        }
    }

    public void Close()
    {
        ChangeActive(gameObject, false);
        ChangeActive(previousWindow, true);
    }
    private void ChangeActive(GameObject window, bool isActive)
    {
        window.SetActive(isActive);
    }
}

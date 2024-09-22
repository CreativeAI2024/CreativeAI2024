using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;


public class CloseWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    public GameObject previousWindow;
    void Start()
    {
        // Debug.Log("CloseWindow on "+gameObject);
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetCancelKeyDown() && previousWindow != null)
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

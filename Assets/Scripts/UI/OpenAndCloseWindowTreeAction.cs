using UnityEngine;

public class OpenAndCloseWindowTreeAction : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject windowBox;
    [SerializeField] private GameObject topWindow;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetItemKeyDown() || (_inputSetting.GetCancelKeyDown()&&topWindow.activeSelf))
        {
            ToggleWindow();
        }
    }
    public void ToggleWindow() //どのウィンドウを開けてるかの状態もリセットしたい。各ウィンドウのdisenable()の中で処理した方がいい？
    {
        ChangeActive(windowBox, !windowBox.activeSelf);
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

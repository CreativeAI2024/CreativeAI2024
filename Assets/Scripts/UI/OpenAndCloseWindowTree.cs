using UnityEngine;

public class OpenAndCloseWindowTree : MonoBehaviour
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
            OpenAndClose();
        }
    }
    public void OpenAndClose() //どのウィンドウを開けてるかの状態もリセットしたい。各ウィンドウのdisenable()の中で処理した方がいい？
    {
        ChangeActive(windowBox, !windowBox.activeSelf);
    }
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

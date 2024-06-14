using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject previousWindow;

    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (_inputSetting.GetCancelKeyDown() && previousWindow != null) //タグで検査するようにする
        {
            Cancel();
        }
    }

    public void Cancel()
    {
        ChangeActive(gameObject, false);
        ChangeActive(previousWindow, true);
    }
    private void ChangeActive(GameObject _gameObject, bool isActive)
    {
        _gameObject.SetActive(isActive);
    }
}

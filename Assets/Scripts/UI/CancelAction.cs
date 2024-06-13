using UnityEngine;

public class CancelAction : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] public GameObject previousWindow;

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
    private void ChangeActive(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

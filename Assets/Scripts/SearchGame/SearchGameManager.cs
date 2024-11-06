using UnityEngine;

public class SearchGameManager : MonoBehaviour
{
    private InputSetting _inputSetting;
    [SerializeField] private GameObject main;
    [SerializeField] private SearchGameCursor searchGameCursor;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }

    void Update()
    {
        if (_inputSetting.GetCancelKeyDown())
        {
            Inactivate();
        }
    }
    public void Activate()
    {
        main.SetActive(true);
    }
    public void Inactivate()
    {
        main.SetActive(false);
        searchGameCursor.Reset();
    }
}

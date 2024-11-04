using UnityEngine;

public class EventTriggerImage : MonoBehaviour
{
    [SerializeField] private InputSetting _inputSetting;
    private bool isFocused = false;
    void Start()
    {
        _inputSetting = InputSetting.Load();
    }
    void Update()
    {
        if (isFocused && _inputSetting.GetDecideKeyDown())
        {
            Debug.Log("Clicked");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        ChangeIsFocused(true);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        ChangeIsFocused(false);
    }

    private void ChangeIsFocused(bool isFocused)
    {
        this.isFocused = isFocused;
    }
}
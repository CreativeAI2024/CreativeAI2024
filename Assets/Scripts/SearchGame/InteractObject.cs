using UnityEngine;

public class InteractObject : MonoBehaviour
{
    private InputSetting _inputSetting;
    private IEffect effect;
    private bool isFocused = false;
    void Start()
    {
        _inputSetting = InputSetting.Load();
        effect = gameObject.GetComponent<IEffect>();
        gameObject.AddComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (isFocused && _inputSetting.GetDecideKeyDown())
        {
            effect.Run();
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
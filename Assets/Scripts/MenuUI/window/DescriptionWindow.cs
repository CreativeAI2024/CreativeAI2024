using TMPro;
using UnityEngine;

public class DescriptionWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    public void SetText(string text)
    {
        textComponent.text = text;
    }
}

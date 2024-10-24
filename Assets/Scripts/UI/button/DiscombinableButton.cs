using TMPro;
using UnityEngine;

public class DiscombinableButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    
    public void Initialize(Item item)
    {
        itemName.text = item.ItemName;
        itemName.color = Color.gray;
    }
}

using UnityEngine;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmYesButton;

    public GameObject ItemImageScreen => itemImageScreen;
    public GameObject ConfirmWindow => confirmWindow;
    public GameObject ConfirmYesButton => confirmYesButton;
}

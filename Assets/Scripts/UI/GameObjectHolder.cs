using UnityEngine;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject confirmYesButton;

    public GameObject ItemImageScreen => itemImageScreen;
    public GameObject ConfirmYesButton => confirmYesButton;
}

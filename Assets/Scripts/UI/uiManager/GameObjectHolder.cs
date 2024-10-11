using UnityEngine;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmYesButton;
    [SerializeField] private GameObject imageShowButtonPrefab;
    [SerializeField] private GameObject itemWindow;

    public GameObject ItemImageScreen => itemImageScreen;
    public GameObject ConfirmWindow => confirmWindow;
    public GameObject ConfirmYesButton => confirmYesButton;
    public GameObject ImageShowButtonPrefab => imageShowButtonPrefab;
    public GameObject ItemWindow => itemWindow;
}

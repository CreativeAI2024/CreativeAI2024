using UnityEngine;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private GameObject actionsWindow;
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmYesButton;
    [SerializeField] private GameObject conversationWindow;
    [SerializeField] private GameObject actionButtonPrefab;
    [SerializeField] private Transform actionButtons;

    public GameObject ItemWindow => itemWindow;
    public GameObject ActionsWindow => actionsWindow;
    public GameObject ItemImageScreen => itemImageScreen;
    public GameObject ConfirmWindow => confirmWindow;
    public GameObject ConfirmYesButton => confirmYesButton;
    public GameObject ConversationWindow => conversationWindow;
    public GameObject ActionButtonPrefab => actionButtonPrefab;
    public Transform ActionButtons => actionButtons;
}

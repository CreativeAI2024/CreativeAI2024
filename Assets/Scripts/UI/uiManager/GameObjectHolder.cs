using UnityEngine;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmYesButton;
    [SerializeField] private GameObject conversationWindow;

    public GameObject ItemWindow => itemWindow;
    public GameObject ItemImageScreen => itemImageScreen;
    public GameObject ConfirmWindow => confirmWindow;
    public GameObject ConfirmYesButton => confirmYesButton;
    public GameObject ConversationWindow => conversationWindow;
}

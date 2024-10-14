using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameObjectHolder : MonoBehaviour
{
    [SerializeField] private GameObject itemWindow;
    [SerializeField] private GameObject actionsWindow;
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private GameObject confirmYesButton;
    [SerializeField] private GameObject conversationWindow;
    [SerializeField] private GameObject displayButton;
    [SerializeField] private GameObject combineButton;
    [SerializeField] private ImageTextButton actionImageTextButtonComponent;
    [SerializeField] private ImageButton actionImageButtonComponent;
    [SerializeField] private TextButton actionTextButtonComponent;
    [SerializeField] private CombineButton actionCombineButtonComponent;
    [SerializeField] private Transform actionButtons;

    public GameObject ItemWindow => itemWindow;
    public GameObject ActionsWindow => actionsWindow;
    public GameObject ItemImageScreen => itemImageScreen;
    public GameObject ConfirmWindow => confirmWindow;
    public GameObject ConfirmYesButton => confirmYesButton;
    public GameObject ConversationWindow => conversationWindow;
    public GameObject DisplayButton => displayButton;
    public GameObject CombineButton => combineButton;
    public ImageTextButton ActionImageTextButtonComponent => actionImageTextButtonComponent;
    public ImageButton ActionImageButtonComponent => actionImageButtonComponent;
    public TextButton ActionTextButtonComponent => actionTextButtonComponent;
    public CombineButton ActionCombineButtonComponent => actionCombineButtonComponent;
    public Transform ActionButtons => actionButtons;
}

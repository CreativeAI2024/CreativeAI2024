using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetActionButtons : MonoBehaviour
{
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    [SerializeField] private GameObject displayButton;
    [SerializeField] private GameObject combineButton;
    [SerializeField] private GameObject itemImageScreen;
    [SerializeField] private GameObject conversationWindow;
    private OpenWindow displayButtonOpenWindow;
    private ImageTextButton imageTextButtonComponent;
    private ImageButton imageButtonComponent;
    private TextButton textButtonComponent;
    private CombineButton combineButtonComponent;
    private Item thisItem;
    public Item ThisItem { set { thisItem = value; } }
    void Awake()
    {
        displayButtonOpenWindow = displayButton.GetComponent<OpenWindow>();
        imageTextButtonComponent = displayButton.GetComponent<ImageTextButton>();
        imageButtonComponent = displayButton.GetComponent<ImageButton>();
        textButtonComponent = displayButton.GetComponent<TextButton>();
        combineButtonComponent = combineButton.GetComponent<CombineButton>();
    }
    void OnEnable()
    {
        if (thisItem.Image != null && thisItem.ContentText.Count != 0)
        {
            SetDisplayButton(conversationWindow, imageTextButtonComponent);
        }
        else if (thisItem.Image != null)
        {
            SetDisplayButton(itemImageScreen, imageButtonComponent);
        }
        else if (thisItem.ContentText.Count != 0)
        {
            SetDisplayButton(conversationWindow, textButtonComponent);
        }
        if (combineRecipeDatabase.GetPairIngredient(thisItem))
        {
            combineButton.SetActive(true);
            //TODO: pairItemを持ってなければselectableをfalseにする
            combineButtonComponent.enabled = true;
            combineButtonComponent.ThisItem = thisItem;
        }
    }

    private void SetDisplayButton(GameObject window, ItemActionButton component)
    {
        displayButton.SetActive(true);
        displayButtonOpenWindow.NextWindow = window;
        component.enabled = true;
        component.ThisItem = thisItem;
    }
}
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeActionButtons : MonoBehaviour
{
    private InputSetting _inputSetting;
    private GameObject actionButtonPrefab;
    private Transform actionButtons;
    private Item thisItem;
    private CombineRecipeDatabase combineRecipeDatabase;
    public Item ThisItem { set { thisItem = value; } }
    void Start()
    {
        _inputSetting = InputSetting.Load();
        GameObjectHolder gameObjectHolder = GameObject.FindWithTag("UIManager").GetComponent<GameObjectHolder>();
        actionButtonPrefab = gameObjectHolder.ActionButtonPrefab;
        actionButtons = gameObjectHolder.ActionButtons;
        combineRecipeDatabase = Resources.Load<CombineRecipeDatabase>("Items/CombineRecipes/CombineRecipeDatabase");

    }
    void Update()
    {
        if (_inputSetting.GetDecideKeyDown())
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                if (thisItem.Image != null && thisItem.Text.Count != 0)
                {
                    //Instantiateじゃなく、事前にオブジェクト配置&setActive()で切り替える方針
                    GameObject actionButton = Instantiate(actionButtonPrefab, actionButtons);
                    actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Display";
                    actionButton.AddComponent<ImageTextButton>().ThisItem = thisItem;
                }
                else if (thisItem.Image != null)
                {
                    GameObject actionButton = Instantiate(actionButtonPrefab, actionButtons);
                    actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Display";
                    actionButton.AddComponent<ImageButton>().ThisItem = thisItem;
                }
                else if (thisItem.Text.Count != 0)
                {
                    GameObject actionButton = Instantiate(actionButtonPrefab, actionButtons);
                    actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Display";
                    actionButton.AddComponent<TextButton>().ThisItem = thisItem;
                }
                if (combineRecipeDatabase.GetPairItem(thisItem))
                {
                    GameObject actionButton = Instantiate(actionButtonPrefab, actionButtons);
                    actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Combine";
                    //TODO: pairItemを持ってなければselectableをfalseにする
                    actionButton.AddComponent<CombineButton>().ThisItem = thisItem;
                }

            }
        }
    }
}
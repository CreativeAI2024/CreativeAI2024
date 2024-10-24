using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemActionWindow : Window
{
    [SerializeField] private CombineRecipeDatabase combineRecipeDatabase;
    private Item _item;
    [SerializeField] private CombineButton combineButton;
    [SerializeField] private ImageTextButton imageTextButton;
    [SerializeField] private ImageButton imageButton;
    [SerializeField] private TextButton textButton;
    
    public void Initialize(Item item)
    {
        _item = item;
    }
    
    private void OnEnable()
    {
        ButtonInactive();
        MakeActionButton();
    }
    
    private void ButtonInactive()
    {
        imageTextButton.gameObject.SetActive(false);
        imageButton.gameObject.SetActive(false);
        textButton.gameObject.SetActive(false);
        combineButton.gameObject.SetActive(false);
    }
    
    private void MakeActionButton()
    {
        Debug.Log("_item.ContentText : "+_item.ContentText);
        //TODO: ここを、「そもそも合成可能か」ではなく、「ペアアイテムを今持っているか」に変える。
        if (combineRecipeDatabase.IsCombinable(_item))
        {
            combineButton.gameObject.SetActive(true);
            combineButton.Initialize(_item, "Combine", base.OnCancelKeyDown, this);
            EventSystem.current.SetSelectedGameObject(combineButton.gameObject);
        }
        if (_item.Sprite != null && _item.ContentText.Any())
        {
            imageTextButton.gameObject.SetActive(true);
            imageTextButton.Initialize(_item, "Display", base.OnCancelKeyDown, this);
            EventSystem.current.SetSelectedGameObject(imageTextButton.gameObject);
        }
        else if (_item.Sprite != null)
        {
            Debug.Log("2");
            imageButton.gameObject.SetActive(true);
            imageButton.Initialize(_item, "Display", base.OnCancelKeyDown, this);
            EventSystem.current.SetSelectedGameObject(imageButton.gameObject);
        }
        else if (_item.ContentText.Any())
        {
            Debug.Log("3");
            textButton.gameObject.SetActive(true);
            textButton.Initialize(_item, "Display", base.OnCancelKeyDown, this);
            EventSystem.current.SetSelectedGameObject(textButton.gameObject);
        }
    }
}
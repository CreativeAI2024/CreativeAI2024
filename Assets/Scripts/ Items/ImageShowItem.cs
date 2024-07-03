using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//ScriptableObject自身が処理するのではなく、あくまでデータを持ち、それを管理する処理だけを作るべき
//ScriptableObjectはシーン内のオブジェクトを直接参照できない SerializeFieldからでも取得できない
[Serializable]
[CreateAssetMenu(fileName ="ImageShowItem", menuName ="CreateImageShowItem")]
public class ImageShowItem : BaseItem
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Sprite image;
    private GameObject imageObject;
    public void Start()
    {
        imageObject = new GameObject("ItemImage");
        imageObject.AddComponent<Image>();
        imageObject.GetComponent<Image>().sprite = image;
        imageObject.transform.SetParent(canvas.transform);
        imageObject.transform.localPosition = Vector3.zero;
        ChangeActive(false);
    }

    public void ShowImage()
    {
        ChangeActive(true);
    }

    public void HideImage()
    {
        ChangeActive(false);
    }

    private void ChangeActive(bool isVisible)
    {
        imageObject.SetActive(isVisible);
    }
}
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
    [SerializeField] private Sprite image;
    public Sprite Image => image;
}
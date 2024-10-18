// using UnityEngine;

// //windowはSelectableコンポーネントをアタッチしてないから良く無いのでは？
// //祭り後にItemButton.csに追記したように、ボタンにIFocusedObjectをつけたほうがいいのでは？
// abstract public class Window : MonoBehaviour
// {
//     [SerializeField] protected GameObject previousWindow;
//     [SerializeField] protected GameObject nextWindow;

//     protected void ChangeActive(GameObject window, bool isActive)
//     {
//         window.SetActive(isActive);
//     }
//     public virtual void OnDecideKeyDown()
//     {
//         ChangeActive(gameObject, false);
//         ChangeActive(nextWindow, true);
//     }
//     public void OnCancelKeyDown()
//     {
//         ChangeActive(gameObject, false);
//         ChangeActive(previousWindow, true);
//     }
// }
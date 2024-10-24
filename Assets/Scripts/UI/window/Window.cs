using UnityEngine;

//windowはSelectableコンポーネントをアタッチしてないから良く無いのでは？
//祭り後にItemButton.csに追記したように、ボタンにIFocusedObjectをつけたほうがいいのでは？
public class Window : MonoBehaviour
{
    [SerializeField] protected GameObject previousWindowObj;
    
    public virtual void OnCancelKeyDown()
    {
        if (previousWindowObj == null) return;
        
        gameObject.SetActive(false);
        previousWindowObj.SetActive(true);
    }
    
    public virtual void OnDecideKeyDown(Window previousWindow = null)
    {
        previousWindow?.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
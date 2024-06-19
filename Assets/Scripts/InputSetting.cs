using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/InputSetting")]
public class InputSetting : ScriptableObject
{
    public KeyCode forwardKey;
    public KeyCode leftKey;
    public KeyCode backKey;
    public KeyCode rightKey;
    public KeyCode decideKey;
    public KeyCode cancelKey;
    public KeyCode itemKey;
    
    public static InputSetting Load(string path = "Player Input Setting")
    {
        return Resources.Load<InputSetting>(path);
    }
    
    public bool GetForwardKeyDown() => Input.GetKeyDown(forwardKey);
    public bool GetForwardKey() => Input.GetKey(forwardKey);
    public bool GetForwardKeyUp() => Input.GetKeyUp(forwardKey);
    
    public bool GetLeftKeyDown() => Input.GetKeyDown(leftKey);
    public bool GetLeftKey() => Input.GetKey(leftKey);
    public bool GetLeftKeyUp() => Input.GetKeyUp(leftKey);
    
    public bool GetBackKeyDown() => Input.GetKeyDown(backKey);
    public bool GetBackKey() => Input.GetKey(backKey);
    public bool GetBackKeyUp() => Input.GetKeyUp(backKey);
    
    public bool GetRightKeyDown() => Input.GetKeyDown(rightKey);
    public bool GetRightKey() => Input.GetKey(rightKey);
    public bool GetRightKeyUp() => Input.GetKeyUp(rightKey);
    
    public bool GetDecideKeyDown() => Input.GetKeyDown(decideKey);
    public bool GetDecideKey() => Input.GetKey(decideKey);
    public bool GetDecideKeyUp() => Input.GetKeyUp(decideKey);
    
    public bool GetCancelKeyDown() => Input.GetKeyDown(cancelKey);
    public bool GetCancelKey() => Input.GetKey(cancelKey);
    public bool GetCancelKeyUp() => Input.GetKeyUp(cancelKey);
    
    public bool GetItemKeyDown() => Input.GetKeyDown(itemKey);
    public bool GetItemKey() => Input.GetKey(itemKey);
    public bool GetItemKeyUp() => Input.GetKeyUp(itemKey);
}
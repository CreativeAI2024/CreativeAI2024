using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName="ScriptableObject/InputSetting")]
public class InputSetting : ScriptableObject
{
    public List<KeyCode> forwardKey;
    public List<KeyCode> leftKey;
    public List<KeyCode> backKey;
    public List<KeyCode> rightKey;
    public List<KeyCode> decideKey;
    public List<KeyCode> cancelKey;
    public List<KeyCode> menuKey;
    
    public static InputSetting Load(string path = "Player Input Setting")
    {
        return Resources.Load<InputSetting>(path);
    }

    private bool GetAnyKeyDown(List<KeyCode> keys)
    {
        foreach (KeyCode key in keys)
        {
            if (!Input.GetKeyDown(key)) continue;
            
            return true;
        }
        return false;
    }
    
    private bool GetAnyKey(List<KeyCode> keys)
    {
        foreach (KeyCode key in keys)
        {
            if (!Input.GetKey(key)) continue;
            
            return true;
        }
        return false;
    }
    
    private bool GetAnyKeyUp(List<KeyCode> keys)
    {
        foreach (KeyCode key in keys)
        {
            if (!Input.GetKeyUp(key)) continue;
            
            return true;
        }
        return false;
    }

    public bool GetForwardKeyDown() => GetAnyKeyDown(forwardKey);
    public bool GetForwardKey() => GetAnyKey(forwardKey);
    public bool GetForwardKeyUp() => GetAnyKeyUp(forwardKey);
    
    public bool GetLeftKeyDown() => GetAnyKeyDown(leftKey);
    public bool GetLeftKey() => GetAnyKey(leftKey);
    public bool GetLeftKeyUp() => GetAnyKeyUp(leftKey);
    
    public bool GetBackKeyDown() => GetAnyKeyDown(backKey);
    public bool GetBackKey() => GetAnyKey(backKey);
    public bool GetBackKeyUp() => GetAnyKeyUp(backKey);
    
    public bool GetRightKeyDown() => GetAnyKeyDown(rightKey);
    public bool GetRightKey() => GetAnyKey(rightKey);
    public bool GetRightKeyUp() => GetAnyKeyUp(rightKey);
    
    public bool GetDecideInputDown() => GetAnyKeyDown(decideKey) || Input.GetMouseButtonDown(0);
    public bool GetDecideInput() => GetAnyKey(decideKey) || Input.GetMouseButton(0);
    public bool GetDecideInputUp() => GetAnyKeyUp(decideKey) || Input.GetMouseButtonUp(0);
    
    public bool GetCancelKeyDown() => GetAnyKeyDown(cancelKey);
    public bool GetCancelKey() => GetAnyKey(cancelKey);
    public bool GetCancelKeyUp() => GetAnyKeyUp(cancelKey);
    
    public bool GetMenuKeyDown() => GetAnyKeyDown(menuKey);
    public bool GetMenuKey() => GetAnyKey(menuKey);
    public bool GetMenuKeyUp() => GetAnyKeyUp(menuKey);
}
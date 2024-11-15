using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponentReferenceTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void ReferenceTestSimplePasses()
    {
        // Use the Assert class to test conditions
        string scenename = "";
        string lastobjname = "";
        int missingScriptCountSum = 0;
        foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            int missingScriptCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj);
            if (missingScriptCount > 0)
            {
                scenename = obj.scene.name;
                lastobjname = obj.name;
                Assert.Fail($"Missing Component: {scenename} > {lastobjname}");
            }
            missingScriptCountSum += missingScriptCount;
        }
        Assert.Zero(missingScriptCountSum, $"Last Missing Component: {scenename} > {lastobjname}");
    }
    
    public static List<GameObject> GetAllSceneObjects()
    {
        var objs = new List<GameObject>();
        var count = SceneManager.sceneCount;
        for (var i = 0; i < count; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            foreach (var obj in scene.GetRootGameObjects())
            {
                FindRecursive(ref objs, obj);
            }
        }
        return objs;
    }
    
    private static void FindRecursive(ref List<GameObject> list, GameObject root)
    {
        list.Add(root);
        foreach (Transform child in root.transform)
        {
            FindRecursive(ref list, child.gameObject);
        }
    }
}

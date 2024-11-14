using System.Collections;
using System.Collections.Generic;
//using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComponentReferenceTest
{
    /*
    // A Test behaves as an ordinary method
    [Test]
    public void ReferenceTestSimplePasses()
    {
        // Use the Assert class to test conditions
        foreach (var obj in GetAllSceneObjects())
        {
            int missingScriptCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj);
            Assert.Zero(missingScriptCount, $"Missing Component: {obj.scene.name} > {obj.name}");
        }
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
    }*/
}

using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ComponentReferenceTest
{
    /*
    // A Test behaves as an ordinary method
    [Test]
    public void ReferenceTestSimplePasses()
    {
        // Use the Assert class to test conditions
        string scenename = "";
        string lastobjname = "";
        int missingScriptCountSum = 0;
        foreach (var obj in Resources.FindObjectsOfTypeAll<GameObject>())//GetAllSceneObjects()
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    //missingScriptCountSum++;
                    scenename = obj.scene.name;
                    lastobjname = obj.name;
                }
                else
                {
                    missingScriptCountSum++;
                }
            }
        }
        Assert.Zero(missingScriptCountSum, $"Last Missing Component: {scenename} > {lastobjname}");
    }*/
    
    private static void FindRecursive(ref List<GameObject> list, GameObject root)
    {
        list.Add(root);
        foreach (Transform child in root.transform)
        {
            FindRecursive(ref list, child.gameObject);
        }
    }
    
    [UnityTest]
    public IEnumerator ReferenceTestSimplePasses()
    {
        // Use the Assert class to test conditions
        //
        string scenename = "";
        string lastobjname = "";
        int missingScriptCountSum = 0;
        AssetDatabase.Refresh();
        yield return new WaitForSeconds(2);
        var objs = new List<GameObject>();
        var count = SceneManager.sceneCountInBuildSettings;
        for (var i = 0; i < count; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            var scene = SceneManager.GetSceneByPath(path);
            EditorSceneManager.OpenScene("Assets/Scenes/Scene2.unity", OpenSceneMode.Additive);
            //yield return SceneManager.LoadSceneAsync(i);
            //var scene = SceneManager.GetActiveScene();
            Debug.Log(scene.name);
            foreach (var obj in scene.GetRootGameObjects())
            {
                FindRecursive(ref objs, obj);
            }
        }
        foreach (var obj in objs)
        {
            Debug.Log(obj.name);
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    //missingScriptCountSum++;
                    scenename = obj.scene.name;
                    lastobjname = obj.name;
                }
                else
                {
                    missingScriptCountSum++;
                }
            }/*
            int missingScriptCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj);
            if (missingScriptCount > 0)
            {
                scenename = obj.scene.name;
                lastobjname = obj.name;
                Assert.Fail($"Missing Component: {scenename} > {lastobjname}");
            }
            missingScriptCountSum += missingScriptCount;*/
        }
        Assert.Zero(missingScriptCountSum, $"Last Missing Component: {scenename} > {lastobjname}");
    }
}

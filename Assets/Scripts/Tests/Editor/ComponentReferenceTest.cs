using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

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
        var objs = new List<GameObject>();
        var count = SceneManager.sceneCountInBuildSettings;
        for (var i = 0; i < count; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
            foreach (var obj in scene.GetRootGameObjects())
            {
                FindRecursive(ref objs, obj);
            }
        }
        StringBuilder resultText = new StringBuilder();
        foreach (var obj in objs)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    missingScriptCountSum++;
                    resultText.AppendLine($"Missing Component: {obj.scene.name} > {obj.name}");
                }
            }
        }
        Assert.Zero(missingScriptCountSum, resultText.ToString());
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

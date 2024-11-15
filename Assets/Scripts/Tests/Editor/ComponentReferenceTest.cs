using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ComponentReferenceTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void MissingReferenceTest()
    {
        // Use the Assert class to test conditions
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
        StringBuilder resultText = new StringBuilder("");
        foreach (var obj in objs)
        {
            int missingCount = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(obj);
            if (missingCount > 0)
            {
                missingScriptCountSum += missingCount;
                resultText.AppendLine($"Missing Component: {obj.scene.name} > {obj.name}");
            }
        }
        Assert.Zero(missingScriptCountSum, resultText.ToString());
    }
    
    [Test]
    public void SerializeMissingTest()
    {
        int missingSerializeFieldCount = 0;
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
        StringBuilder resultText = new StringBuilder("");
        foreach (var obj in objs)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null) continue;
                var serializedProp = new SerializedObject(component).GetIterator();
                
                while (serializedProp.NextVisible(true))
                {
                    if (serializedProp.propertyType != SerializedPropertyType.ObjectReference) continue;
                    if (serializedProp.objectReferenceValue != null) continue;
                    
                    var fileId = serializedProp.FindPropertyRelative("m_FileID");
                    if (fileId == null || fileId.intValue == 0) continue;
                    
                    if (serializedProp.propertyPath.Equals("m_fontAsset") || serializedProp.propertyPath.Equals("m_sharedMaterial")) continue;// fontをgitignoreに入れているので飛ばす
                    
                    missingSerializeFieldCount++;
                    resultText.AppendLine($"Missing Component SerializeField: {obj.scene.name} > {obj.name} > {component.name} > {serializedProp.propertyPath}");
                }
            }
        }
        Assert.Zero(missingSerializeFieldCount, resultText.ToString());
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

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AttachHelper.Editor
{
    public class AttachHelper : EditorWindow
    {
        public class PropertyComparer : IEqualityComparer<UniquePropertyInfo>
        {
            public bool Equals(UniquePropertyInfo x, UniquePropertyInfo y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                return x.GlobalObjectIdString.Equals(y.GlobalObjectIdString) && x.PropertyPath.Equals(y.PropertyPath);
            }

            public int GetHashCode(UniquePropertyInfo obj)
            {
                unchecked
                {
                    return ((obj.GlobalObjectIdString != null ? obj.GlobalObjectIdString.GetHashCode() : 0) * 397) ^ (obj.PropertyPath != null ? obj.PropertyPath.GetHashCode() : 0);
                }
            }
        }
        public class UniqueProperty : UniquePropertyInfo
        {
            public SerializedProperty SerializedProperty { get; }
        
            public UniqueProperty(string globalObjectIdString, SerializedProperty property) : base(globalObjectIdString, property.propertyPath)
            {
                SerializedProperty = property.Copy();
            }
        }

        public class UniquePropertyInfo
        {
            public string GlobalObjectIdString { get; }
            public string PropertyPath { get; }
        
            public UniquePropertyInfo(string globalObjectIdString, string propertyPath)
            {
                GlobalObjectIdString = globalObjectIdString;
                PropertyPath = propertyPath;
            }
        }
    
        /// <summary>
        /// Noneなやつ
        /// </summary>
        static List<UniqueProperty> show = new();
    
        /// <summary>
        /// showに登録されたやつを検索するためのやつ
        /// </summary>
        private static HashSet<UniquePropertyInfo> showcomp = new HashSet<UniquePropertyInfo>(new PropertyComparer());
    
        /// <summary>
        /// Noneだけどそれでいいから無視するやつ。Noneじゃなくなってもそのまま。
        /// </summary>
        private static HashSet<UniquePropertyInfo> ignores = new HashSet<UniquePropertyInfo>(new PropertyComparer());

        private Vector2 _scrollPosition = Vector2.zero;
    
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            EditorSceneManager.sceneOpened += (_, _) =>
            {
                show.Clear();
                showcomp.Clear();
                RestoreData();
                RegisterSerializeNone();
            };
            
            RestoreData();
            RegisterSerializeNone();
            
            if (HasOpenInstances<AttachHelper>()) {
                FocusWindowIfItsOpen<AttachHelper>();
            }
            else
            {
                if (!IsShowAny()) return;
                AttachHelper window = GetWindow<AttachHelper>();
                window.Show();
            }
        }

        static bool IsShowAny()
        {
            foreach (UniqueProperty serializedObj in show)
            {
                if (ignores.Contains(serializedObj)) continue;
                return true;
            }
            return false;
        }
    
        [MenuItem("AttachHelper/Check")]
        public static void ShowWindow()
        {
            RestoreData();
            RegisterSerializeNone();
            if (HasOpenInstances<AttachHelper>()) {
                FocusWindowIfItsOpen<AttachHelper>();
            }
            else
            {
                AttachHelper window = GetWindow<AttachHelper>();
                window.Show();
            }
        }
    
        [MenuItem("AttachHelper/Reset")]
        public static void Clear()
        {
            show.Clear();
            showcomp.Clear();
            ignores.Clear();
            ClearData();
        }

        private static void ClearData()
        {
            if (string.IsNullOrEmpty(EditorUserSettings.GetConfigValue("ignoreCount")))
            {
                return;
            }
            int ignoreCount = int.Parse(EditorUserSettings.GetConfigValue("ignoreCount"));
            for (int i = 0; i < ignoreCount; i++)
            {
                EditorUserSettings.SetConfigValue($"globalObjectIdString{i}", null);
                EditorUserSettings.SetConfigValue($"propertyPath{i}", null);
            }
            EditorUserSettings.SetConfigValue("ignoreCount", null);
        }
    
        private static void RestoreData()
        {
            if (string.IsNullOrEmpty(EditorUserSettings.GetConfigValue("ignoreCount")))
            {
                EditorUserSettings.SetConfigValue("ignoreCount", "0");
            }
        
            int ignoreCount = int.Parse(EditorUserSettings.GetConfigValue("ignoreCount"));
            for (int i = 0; i < ignoreCount; i++)
            {
                string globalObjectId = EditorUserSettings.GetConfigValue($"globalObjectIdString{i}");
                string propertyPath = EditorUserSettings.GetConfigValue($"propertyPath{i}");
                ignores.Add(new UniquePropertyInfo(globalObjectId, propertyPath));
            }
        }
        
        private static void AddIgnore(UniquePropertyInfo uniquePropertyInfo)
        {
            ignores.Add(uniquePropertyInfo);
        
            int ignoreCount = int.Parse(EditorUserSettings.GetConfigValue("ignoreCount"));
            EditorUserSettings.SetConfigValue($"globalObjectIdString{ignoreCount}", uniquePropertyInfo.GlobalObjectIdString);
            EditorUserSettings.SetConfigValue($"propertyPath{ignoreCount}", uniquePropertyInfo.PropertyPath);
            EditorUserSettings.SetConfigValue("ignoreCount", (ignoreCount + 1).ToString());
        }
    
        static void RegisterSerializeNone()
        {
            var scene = SceneManager.GetActiveScene();
            foreach (var obj in scene.GetRootGameObjects())
            {
                FindRecursive(obj);
            }
        }
        
        private static void FindRecursive(GameObject root)
        {
            AddShowSerializeNone(root);
            
            foreach (Transform child in root.transform)
            {
                FindRecursive(child.gameObject);
            }
        }

        static void AddShowSerializeNone(GameObject obj)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null) continue;
                
                var serializedObj = new SerializedObject(component);
                
                var serializedProp = serializedObj.GetIterator();
                while (serializedProp.NextVisible(true))
                {
                    GlobalObjectId globalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(component);
                    string globalObjectIdString = globalObjectId.ToString();
                    UniqueProperty uniqueProperty = new UniqueProperty(globalObjectIdString, serializedProp);
                    if (serializedProp.propertyType != SerializedPropertyType.ObjectReference) continue;
                    if (serializedProp.objectReferenceValue != null) continue;
                    if (showcomp.Contains(uniqueProperty)) continue;

                    show.Add(uniqueProperty);
                    showcomp.Add(uniqueProperty);
                }
            }
        }
    
        void OnGUI()
        {
            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(_scrollPosition, false, false))
            {
                _scrollPosition = scrollViewScope.scrollPosition;
                List<UniqueProperty> uniqueProperties = new List<UniqueProperty>();
                List<GlobalObjectId> globalObjectIdList = new List<GlobalObjectId>();
                foreach (var serializedObj in show)
                {
                    if (ignores.Contains(serializedObj)) continue;

                    uniqueProperties.Add(serializedObj);
                    if (GlobalObjectId.TryParse(serializedObj.GlobalObjectIdString, out GlobalObjectId globalObjectId))
                    {
                        globalObjectIdList.Add(globalObjectId);
                    }
                }
                var objs = new UnityEngine.Object[globalObjectIdList.Count];
                GlobalObjectId.GlobalObjectIdentifiersToObjectsSlow(globalObjectIdList.ToArray(), objs);
                
                for (int i = 0; i < uniqueProperties.Count; i++)
                {
                    var serializedObj = uniqueProperties[i];
                    if (objs[i] == null)
                    {
                        ignores.Remove(serializedObj);
                        continue;
                    }
                    
                    var serializedProp = serializedObj.SerializedProperty;
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        Component component = objs[i] as Component;
                        if (component is null)
                        {
                            throw new NullReferenceException();
                        }

                        GameObject gameObj = component.gameObject;
                        if (GUILayout.Button("Inspect", GUILayout.Width(100)))
                        {
                            Selection.activeGameObject = gameObj;
                        }

                        GUILayout.Label($"{gameObj.name} > {component.GetType()} > {serializedProp.displayName}",
                            GUILayout.MinWidth(200));

                        GUILayout.FlexibleSpace();
                        EditorGUILayout.PropertyField(serializedProp, new GUIContent(GUIContent.none), true,
                            GUILayout.MinWidth(150), GUILayout.MaxWidth(200), GUILayout.ExpandWidth(false));
                        serializedProp.serializedObject.ApplyModifiedProperties();
                        if (GUILayout.Button("Decide", GUILayout.Width(100)))
                        {
                            AddIgnore(serializedObj);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }
            }
        
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Decide All None"))
                {
                    foreach (var serializedObj in show)
                    {
                        if (ignores.Contains(serializedObj)) continue;
                        var serializedProp = serializedObj.SerializedProperty;
                        if (serializedProp.objectReferenceValue != null) continue;
                        AddIgnore(serializedObj);
                    }
                
                    AssetDatabase.SaveAssets();
                }
            
                if (GUILayout.Button("Decide All Attached"))
                {
                    foreach (var serializedObj in show)
                    {
                        if (ignores.Contains(serializedObj)) continue;
                        var serializedProp = serializedObj.SerializedProperty;
                        if (serializedProp.objectReferenceValue == null) continue;
                        AddIgnore(serializedObj);
                    }
                
                    AssetDatabase.SaveAssets();
                }
            }
        
            if (GUILayout.Button("Decide All", GUILayout.Height(40)))
            {
                foreach (var serializedObj in show)
                {
                    if (ignores.Contains(serializedObj)) continue;
                
                    AddIgnore(serializedObj);
                }
            
                AssetDatabase.SaveAssets();
            }
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Close"))
            {
                Close();
            }
        }
    }
}
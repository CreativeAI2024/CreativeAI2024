using System.Collections.Generic;
using UnityEditor;

public class CIDevelopmentBuild
{
    [MenuItem("CIBuild/DevelopmentBuild", false)]
    public static void DevelopmentBuild()
    {
        // 現在のビルドターゲットの取得
        var	buildTarget = EditorUserBuildSettings.activeBuildTarget;
        if ( buildTarget == BuildTarget.StandaloneWindows ) buildTarget = BuildTarget.StandaloneWindows64;
        
        // 登録されているシーンを取得
        List<string> sceneList = new List<string>();
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if ( scene.enabled ) {
                sceneList.Add(scene.path);
            }
        }
        string buildPath = $"../build/{buildTarget.ToString()}/{buildTarget.ToString()}.exe";
        // プレイヤー作成
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = sceneList.ToArray(),
            locationPathName = buildPath,
            target = buildTarget,
            options = BuildOptions.Development // Development Buildを有効化
        };
        
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}

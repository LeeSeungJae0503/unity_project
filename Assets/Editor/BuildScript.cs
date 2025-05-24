using UnityEditor;
using UnityEngine;

public class BuildScript
{
    public static void PerformBuild()
    {
        // 현재 존재하는 씬 목록 (경로는 프로젝트 루트 기준)
        string[] scenes = new[]
        {
            "Assets/Scenes/SampleScene.unity",
            "Assets/Scenes/New Scene.unity",
            "Assets/Scenes/sceneB.unity",
            "Assets/Scenes/sceneA.unity"
        };

        // 빌드 설정
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "Build/LinuxBuild/UnityApp.x86_64",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        // 빌드 실행
        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        // 결과에 따라 종료 처리
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.LogError("❌ Build Failed!");
            EditorApplication.Exit(1);  // 실패 시 Jenkins에서 실패 처리
        }
        else
        {
            Debug.Log("✅ Build Succeeded!");
        }
    }
}

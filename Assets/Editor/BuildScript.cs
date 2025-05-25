using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ✅ 씬 목록 설정 (상대 경로로 정확히 지정)
        string[] scenes = new[]
        {
            "Assets/Scenes/HelloWorld.unity", // 실제 존재하는 씬으로 설정
            "Assets/Scenes/SampleScene.unity",
            "Assets/Scenes/sceneA.unity",
            "Assets/Scenes/sceneB.unity"
        };

        Debug.Log("📦 [Build Scenes List]");
        foreach (var scene in scenes)
        {
            Debug.Log($" - {scene}");
        }

        // ✅ 빌드 설정
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "Build/LinuxBuild/UnityApp.x86_64",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        // ✅ 빌드 실행
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        Debug.Log($"🧾 Build result: {summary.result}");
        Debug.Log($"⏱ Total build time: {summary.totalTime}");
        Debug.Log($"📁 Output path: {summary.outputPath}");

        // ✅ 빌드 실패 시 Jenkins에서 실패 처리
        if (summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("❌ Build Failed!");
            EditorApplication.Exit(1); // Jenkins에서 실패 처리
        }
        else
        {
            Debug.Log("✅ Build Succeeded!");
        }
    }
}

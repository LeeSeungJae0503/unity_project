using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        Debug.Log("🔧 Build started...");
        
        string[] scenes = new[] { "Assets/Scenes/sceneA.unity" }; // 실제 존재하는 씬 사용
        string buildPath = "Build/LinuxBuild/UnityApp.x86_64";

        // 디렉토리 미리 생성
        Directory.CreateDirectory("Build/LinuxBuild");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        Debug.Log($"📦 Build result: {report.summary.result}");
        Debug.Log($"📄 Total errors: {report.summary.totalErrors}");
        Debug.Log($"⏱ Duration: {report.summary.totalTime}");

        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("❌ Build Failed!");
            EditorApplication.Exit(1);
        }
        else
        {
            Debug.Log("✅ Build Succeeded!");
        }
    }
}

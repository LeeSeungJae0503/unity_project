using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        Debug.Log("🔧 Build started...");

        string[] scenes = new[] { "Assets/Scenes/sceneA.unity" };

        // 디렉토리 생성
        string buildDir = "Build/LinuxBuild";
        Directory.CreateDirectory(buildDir);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildDir + "/UnityApp.x86_64",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

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

using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildScript
{
    public static void PerformBuild()
    {
        string[] scenes =
        {
            "Assets/Scenes/SampleScene.unity",
            "Assets/Scenes/New Scene.unity",
            "Assets/Scenes/sceneB.unity",
            "Assets/Scenes/sceneA.unity",
            "Assets/Scenes/HelloWorld.unity"
        };

        Debug.Log("📦 [Build Scenes List]");
        foreach (var s in scenes)
            Debug.Log($" • {s}");

        var opts = new BuildPlayerOptions
        {
            scenes           = scenes,
            locationPathName = "Build/LinuxBuild/UnityApp.x86_64",
            target           = BuildTarget.StandaloneLinux64,
            options          = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(opts);

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

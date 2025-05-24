using UnityEditor;
using UnityEngine;

public class BuildScript
{
    public static void PerformBuild()
    {
        string[] scenes = new[] { "Assets/Scenes/HelloWorld.unity" };

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = "Build/LinuxBuild/UnityApp.x86_64",
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.LogError("Build Failed!");
            EditorApplication.Exit(1);  // 실패 시 Jenkins 빌드 실패 처리
        }
        else
        {
            Debug.Log("✅ Build Succeeded!");
        }
    }
}

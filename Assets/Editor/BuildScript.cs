using UnityEditor;
using UnityEngine;
using System.Linq;

public class BuildScript
{
    public static void PerformBuild()
    {
        // Build Settings에 등록된 씬 목록 자동 수집
        string[] scenes = EditorBuildSettings.scenes
                                .Where(s => s.enabled)
                                .Select(s => s.path)
                                .ToArray();

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
            Debug.LogError("❌ Build Failed!");
            EditorApplication.Exit(1); // Jenkins에 실패 전달
        }
        else
        {
            Debug.Log("✅ Build Succeeded!");
            EditorApplication.Exit(0); // Jenkins에 성공 전달
        }
    }
}

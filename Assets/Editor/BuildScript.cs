// Assets/Editor/BuildScript.cs
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildScript
{
    public static void PerformBuild()
    {
        // 🔖 프로젝트에 실제로 존재하는 씬 경로만 넣어 주세요
        string[] scenes =
        {
            "Assets/Scenes/SampleScene.unity",
            "Assets/Scenes/New Scene.unity",
            "Assets/Scenes/sceneB.unity",
            "Assets/Scenes/sceneA.unity",
            "Assets/Scenes/HelloWorld.unity"
        };

        /* ───────────────────────────────────────────────
           빌드 전 확인용 : Jenkins 로그에 출력
        ─────────────────────────────────────────────── */
        Debug.Log("📦 [Build Scenes List]");
        foreach (var s in scenes)
            Debug.Log($" • {s}");

        /* ─────────────────────────────────────────────── */

        var opts = new BuildPlayerOptions
        {
            scenes            = scenes,
            locationPathName  = "Build/LinuxBuild/UnityApp.x86_64",
            target            = BuildTarget.StandaloneLinux64,
            options           = BuildOptions.None
        };

        BuildReport report = BuildPipeline.BuildPlayer(opts);

        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("❌ Build Failed!");
            EditorApplication.Exit(1);      // ➡️ Jenkins 에서 실패 처리
        }
        else
        {
            Debug.Log("✅ Build Succeeded!");
        }
    }
}

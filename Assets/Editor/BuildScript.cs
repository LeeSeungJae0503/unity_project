// using UnityEditor;
// using UnityEngine;

// public class BuildScript
// {
//     public static void PerformBuild()
//     {
//         string[] scenes = { "Assets/Scenes/MainScene.unity" };
//         string buildPath = "Build/LinuxBuild/UnityApp.x86_64";

//         BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
//         {
//             scenes = scenes,
//             locationPathName = buildPath,
//             target = BuildTarget.StandaloneLinux64,
//             options = BuildOptions.None
//         };

//         Debug.Log("🔨 Unity 빌드 시작 (Linux)...");

//         BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
//         BuildSummary summary = report.summary;

//         if (summary.result == BuildResult.Succeeded)
//         {
//             Debug.Log($"✅ 빌드 성공: {summary.totalSize} bytes");
//         }
//         else if (summary.result == BuildResult.Failed)
//         {
//             Debug.LogError("❌ 빌드 실패");
//             EditorApplication.Exit(1); // Jenkins에서 실패로 인식
//         }
//     }
// }


// 2차
// using UnityEditor;
// using UnityEditor.Build.Reporting;
// using UnityEngine;
// using System;
// using System.Linq;

// public class BuildScript
// {
//     public static void PerformBuild()
//     {
//         Debug.Log("--- BuildScript.PerformBuild 시작 ---");

//         string[] enabledScenes = EditorBuildSettings.scenes
//             .Where(s => s.enabled)
//             .Select(s => s.path)
//             .ToArray();

//         if (enabledScenes.Length == 0)
//         {
//             Debug.LogError("❌ 빌드할 씬이 없습니다. Unity 에디터의 Build Settings에서 씬을 추가하고 체크해주세요.");
//             EditorApplication.Exit(1);
//             return;
//         }

//         // 포함된 씬 목록을 로그에 명확히 출력
//         Debug.Log($"[Build Info] 포함된 씬 개수: {enabledScenes.Length}");
//         Debug.Log($"[Build Info] 포함된 씬 목록: {string.Join(", ", enabledScenes)}");

//         string buildPath = Environment.GetEnvironmentVariable("BUILD_FILE");

//         if (string.IsNullOrEmpty(buildPath))
//         {
//             Debug.LogWarning("⚠️ BUILD_FILE 환경변수를 찾을 수 없습니다. 기본 경로로 빌드합니다.");
//             buildPath = "Build/LinuxBuild/UnityApp.x86_64";
//         }

//         // 빌드 경로를 로그에 명확히 출력
//         Debug.Log($"[Build Info] 최종 빌드 경로: {buildPath}");

//         BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
//         {
//             scenes = enabledScenes,
//             locationPathName = buildPath,
//             target = BuildTarget.StandaloneLinux64,
//             options = BuildOptions.None
//         };

//         Debug.Log("🔨 Unity 빌드 시작 (Linux)...");

//         BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
//         BuildSummary summary = report.summary;

//         if (summary.result == BuildResult.Succeeded)
//         {
//             Debug.Log($"✅ 빌드 성공: {summary.totalSize} bytes");
//         }
//         else if (summary.result == BuildResult.Failed)
//         {
//             Debug.LogError($"❌ 빌드 실패: 총 {summary.errorCount}개의 오류 발생");
//             EditorApplication.Exit(1);
//         }

//         Debug.Log("--- BuildScript.PerformBuild 종료 ---");
//     }
// }

using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.Linq;

public class BuildScript
{
    public static void PerformBuild()
    {
        Debug.Log("--- BuildScript.PerformBuild 시작 ---");

        // Build Settings에 등록되고 활성화된 씬 목록을 자동으로 가져옵니다.
        string[] enabledScenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        // 빌드할 씬이 하나도 없으면 오류를 내고 종료합니다.
        if (enabledScenes.Length == 0)
        {
            Debug.LogError("❌ 빌드할 씬이 없습니다. Unity 에디터의 Build Settings에서 씬을 추가하고 체크해주세요.");
            EditorApplication.Exit(1); // Jenkins에서 실패로 인식
            return;
        }

        // Jenkinsfile의 환경변수에서 빌드 경로를 가져옵니다.
        string buildPath = Environment.GetEnvironmentVariable("BUILD_FILE");

        if (string.IsNullOrEmpty(buildPath))
        {
            Debug.LogWarning("⚠️ BUILD_FILE 환경변수를 찾을 수 없습니다. 기본 경로로 빌드합니다.");
            buildPath = "Build/LinuxBuild/UnityApp.x86_64";
        }
        
        Debug.Log($"[Build Info] 포함된 씬 개수: {enabledScenes.Length}");
        Debug.Log($"[Build Info] 포함된 씬 목록: {string.Join(", ", enabledScenes)}");
        Debug.Log($"[Build Info] 최종 빌드 경로: {buildPath}");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = enabledScenes,
            locationPathName = buildPath,
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        Debug.Log("🔨 Unity 빌드 시작 (Linux)...");

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"✅ 빌드 성공: {summary.totalSize} bytes");
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"❌ 빌드 실패: 총 {summary.errorCount}개의 오류 발생");
            EditorApplication.Exit(1);
        }

        Debug.Log("--- BuildScript.PerformBuild 종료 ---");
    }
}
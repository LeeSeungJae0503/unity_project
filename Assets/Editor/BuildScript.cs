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

// Assets/Editor/BuildScript.cs

// BuildScript.cs (최종 수정안 - 단순화 버전)
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class BuildScript
{
    public static void PerformBuild()
    {
        Debug.Log("--- BuildScript.PerformBuild 시작 ---");

        string[] enabledScenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        if (enabledScenes.Length == 0)
        {
            Debug.LogError("❌ 빌드할 씬이 없습니다. Build Settings에서 씬을 추가하고 체크해주세요.");
            EditorApplication.Exit(1);
            return;
        }

        // [수정] Jenkinsfile에서 빌드 '폴더' 경로를 가져옵니다.
        string buildOutputFolder = Environment.GetEnvironmentVariable("BUILD_OUTPUT");

        if (string.IsNullOrEmpty(buildOutputFolder))
        {
            Debug.LogWarning("⚠️ BUILD_OUTPUT 환경변수를 찾을 수 없습니다. 기본 경로 'Build/LinuxBuild'로 빌드합니다.");
            buildOutputFolder = "Build/LinuxBuild";
        }

        // 빌드 전 기존 폴더를 삭제하여 항상 깨끗한 상태에서 빌드합니다.
        if (Directory.Exists(buildOutputFolder))
        {
            Directory.Delete(buildOutputFolder, true);
        }
        Directory.CreateDirectory(buildOutputFolder);
        
        Debug.Log($"[Build Info] 포함된 씬 개수: {enabledScenes.Length}");
        Debug.Log($"[Build Info] 최종 빌드 경로: {buildOutputFolder}");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = enabledScenes,
            // [수정] locationPathName에 폴더 경로와 실행 파일 이름을 조합하여 전달합니다.
            locationPathName = Path.Combine(buildOutputFolder, "GwangjuRun.x86_64"),
            target = BuildTarget.StandaloneLinux64,
            options = BuildOptions.None
        };

        Debug.Log($"🔨 Unity 빌드 시작 (StandaloneLinux64)...");

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"✅ 빌드 성공: {summary.totalSize} bytes");
        }
        else
        {
            // [수정] 호환성 문제가 있던 errorCount를 사용하지 않습니다.
            Debug.LogError("❌ 빌드 실패: 자세한 내용은 빌드 로그를 확인하세요.");
            EditorApplication.Exit(1);
        }

        Debug.Log("--- BuildScript.PerformBuild 종료 ---");
    }
}

// using UnityEditor;
// using UnityEditor.Build.Reporting;
// using UnityEngine;
// using System;
// using System.IO;
// using System.Linq;

// public class BuildScript
// {
//     public static void PerformBuild()
//     {
//         Debug.Log("--- BuildScript.PerformBuild 시작 ---");

//         /* ───────── 1. 씬 수집 ───────── */
//         string[] enabledScenes = EditorBuildSettings.scenes
//             .Where(s => s.enabled)
//             .Select(s => s.path)
//             .ToArray();

//         if (enabledScenes.Length == 0)
//         {
//             Debug.LogError("❌ 빌드할 씬이 없습니다. Build Settings 확인!");
//             EditorApplication.Exit(1);
//             return;
//         }

//         /* ───────── 2. 환경변수 ───────── */
//         string buildOutput = Environment.GetEnvironmentVariable("BUILD_OUTPUT") ?? "Build/Unknown";
//         string targetFlag  = Environment.GetEnvironmentVariable("UNITY_TARGET") ?? "linux";   // linux|windows

//         /* 디렉터리 초기화 */
//         if (Directory.Exists(buildOutput))
//             Directory.Delete(buildOutput, true);
//         Directory.CreateDirectory(buildOutput);

//         /* ───────── 3. 타깃 선택 ───────── */
//         BuildTarget target;
//         string exeName;
//         if (targetFlag.Equals("windows", StringComparison.OrdinalIgnoreCase))
//         {
//             target   = BuildTarget.StandaloneWindows64;
//             exeName  = "GwangjuRun.exe";
//         }
//         else
//         {
//             target   = BuildTarget.StandaloneLinux64;
//             exeName  = "GwangjuRun.x86_64";
//         }

//         string finalPath = Path.Combine(buildOutput, exeName);
//         Debug.Log($"[Build] Target={target}, Output={finalPath}");

//         /* ───────── 4. BuildPlayer 옵션 ───────── */
//         var opts = new BuildPlayerOptions
//         {
//             scenes = enabledScenes,
//             locationPathName = finalPath,
//             target  = target,
//             options = BuildOptions.None
//         };

//         /* ───────── 5. Build 수행 ───────── */
//         BuildReport report = BuildPipeline.BuildPlayer(opts);
//         if (report.summary.result == BuildResult.Succeeded)
//         {
//             Debug.Log($"✅ 빌드 성공: {report.summary.totalSize} bytes");
//         }
//         else
//         {
//             Debug.LogError("❌ 빌드 실패! 로그 확인");
//             EditorApplication.Exit(1);
//         }
//         Debug.Log("--- BuildScript.PerformBuild 종료 ---");
//     }
// }

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public static class IOSPostProcess
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string pbxProjectPath = PBXProject.GetPBXProjectPath(buildPath);
            PBXProject project = new PBXProject();
            project.ReadFromFile(pbxProjectPath);

            project.SetBuildProperty(project.ProjectGuid(), "VALIDATE_WORKSPACE", "YES");
            project.SetBuildProperty(project.ProjectGuid(), "CLANG_ENABLE_MODULES", "YES");
            project.SetBuildProperty(project.ProjectGuid(), "CLANG_WARN_QUOTED_INCLUDE_IN_FRAMEWORK_HEADER", "YES");
            project.SetBuildProperty(project.ProjectGuid(), "ALWAYS_SEARCH_USER_PATHS", "NO");

            project.WriteToFile(pbxProjectPath);
        }
    }
}
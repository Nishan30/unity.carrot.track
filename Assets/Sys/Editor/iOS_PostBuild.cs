

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Sys.Editor
{
    public static class iOS_PostBuild
    {
        // Replace a windows path with the mac-equivalent path (bug in Unity 2022.x leading to xcode build error on mac)
        // Ex.:
        //  Replace
        //      --data-folder=\"C:/Users/vm/Desktop/unity.carrot.track2/Library/Bee/artifacts/iOS/il2cppOutput/data\"
        //  With
        //      --data-folder=\"$PROJECT_DIR/Data/Managed\"
        //  (keep backslashes)
        // Thanks: https://issuetracker.unity3d.com/issues/cannot-archive-build-for-ios-using-xcode-13-dot-3-when-the-project-is-built-on-a-windows-machine
        [PostProcessBuild]
        static void FixDataFolder_XCProj(BuildTarget buildTarget, string pathToBuiltProject)
        {
            if (buildTarget != BuildTarget.iOS)
                return;

            var xcProj = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
            var xcProjText = File.ReadAllText(xcProj);

            var tok = "--data-folder=\\\"";
            var iTokStart = xcProjText.IndexOf(tok);
            if (iTokStart == -1)
                return;

            var afterTok = xcProjText.Substring(iTokStart + tok.Length);

            var tokTerm = "\\\"";
            var iTokTerm = afterTok.IndexOf(tokTerm);
            if (iTokTerm == -1)
                return;

            var toReplace = tok + afterTok.Substring(0, iTokTerm + tokTerm.Length);
            var correctPath = "$PROJECT_DIR/Data/Managed";
            var replaceWith = tok + correctPath + tokTerm;
            var xcProjTextNew = xcProjText.Replace(toReplace, replaceWith);

            Debug.Log(
                $"Replacing '{toReplace}' with '{replaceWith}' (Unity 2022.x bugfix leading to xcode build error on mac). " +
                $"Thanks: https://issuetracker.unity3d.com/issues/cannot-archive-build-for-ios-using-xcode-13-dot-3-when-the-project-is-built-on-a-windows-machine"
            );
            File.WriteAllText(xcProj, xcProjTextNew);
        }

        [MenuItem("Tools/Debug_iOS_PostBuild__FixDataFolder_XCProj")]
        public static void Debug_iOS_PostBuild__FixDataFolder_XCProj()
        {
            FixDataFolder_XCProj(BuildTarget.iOS, "Build/iOS_Test");
        }
    }
}
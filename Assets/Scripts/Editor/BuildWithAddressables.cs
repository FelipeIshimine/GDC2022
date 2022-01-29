using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public static class BuildWithAddressables
{
    /*
    [MenuItem("Build/Full Addressable and Player Build")]
    public static void FullBuild()
    {
        string result = string.Empty;
        try
        {
            result = EditorUtility.OpenFolderPanel("Select file destination", Application.dataPath, "apk");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        AddressableAssetSettings.BuildPlayerContent();
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, result + "build.apk", EditorUserBuildSettings.activeBuildTarget, BuildPipeline.);
    }*/
}

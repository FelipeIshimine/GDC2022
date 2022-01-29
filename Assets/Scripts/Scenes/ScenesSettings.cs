using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ScenesSettings : RuntimeScriptableSingleton<ScenesSettings>
{
    public AssetReference sceneOne;
    
    public AssetReference[] levels = Array.Empty<AssetReference>();

    public AssetReference mainMenu;
    public static AssetReference MainMenu => Instance.mainMenu;

    public static AssetReference GetLevelReference(int index) => Instance.levels[index];

    public static bool Exists(int intValue) => intValue < Instance.levels.Length;
}


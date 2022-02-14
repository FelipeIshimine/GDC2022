using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ScenesSettings : RuntimeScriptableSingleton<ScenesSettings>
{
    public AssetReference[] levels = Array.Empty<AssetReference>();

    public AssetReference mainMenu;
    public AssetReference mainGame;
    public AssetReference ending;

    public static AssetReference MainMenu => Instance.mainMenu;
    public static AssetReference MainGame => Instance.mainGame;
    public static AssetReference[] Levels => Instance.levels;
    public static AssetReference EndingScene => Instance.ending;

    public static AssetReference GetLevelReference(int index) => Instance.levels[index];

    public static bool Exists(int intValue) => intValue < Instance.levels.Length;
}
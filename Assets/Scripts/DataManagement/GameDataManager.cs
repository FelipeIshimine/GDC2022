using System;
using System.Collections;
using System.Collections.Generic;
using Leguar.TotalJSON;
using SaveSystem;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameDataManager : RuntimeScriptableSingleton<GameDataManager>
{
    public static Action OnLoadDone;
    public static Action OnSaveDone;

    private static Action<JSON> _saveData;
    private static Action<JSON> _loadData;

    public bool enableAutoSave = true;
    [ShowInInspector] private bool _isAutoSaveActive = true;
    

#if UNITY_EDITOR
        public const string FileName = "GameData.json";
#else
        public const string FileName = "GameData.dat";
#endif
    
    private void Awake()
    {
        loadMode = AssetMode.Addressable;
    }

    [Button]
    public static void Save()
    {
        var data = new JSON();
        _saveData?.Invoke(data);
        SaveLoadManager.SaveEncryptedJson(data, FileName);
    }

    [Button]
    public static void Load()
    {
        JSON data  = SaveLoadManager.LoadEncryptedJson( FileName);
        _loadData?.Invoke(data);
    }

    public static void Register(ISaveLoadAsJson value)
    {
        _saveData += value.SaveData;
        _loadData += value.LoadData;
    }
    
    public static void Unregister(ISaveLoadAsJson value)
    {
        _saveData -= value.SaveData;
        _loadData -= value.LoadData;
    }

    public static void RequestAutoSave()
    {
        if(Instance.enableAutoSave && Instance._isAutoSaveActive)
            Save();
    }
    
    public static void SetAutoSave(bool value) => Instance._isAutoSaveActive = value;
}
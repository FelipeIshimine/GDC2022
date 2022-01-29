using System;
using System.Collections.Generic;
using Leguar.TotalJSON;
using SaveSystem;
using Sirenix.OdinInspector;

public class ProgressManager : RuntimeScriptableSingleton<ProgressManager>, ISaveLoadAsJson
{
    [ShowInInspector,ReadOnly] private int _level = 0;
    public static int LevelIndex => Instance._level;

    public override void InitializeSingleton()
    {
        base.InitializeSingleton();
        _level = 0;
    }

    #region SaveLoad
    public JSON GetSave()
    {
        JSON data = new JSON();
        data.Add("Level", _level);
        return data;
    }

    public void Load(JSON data)
    {
        if (data.TryGet("Level", out JNumber jLevel)) _level = jLevel.AsInt();
    }

    public int CurrentVersion => 1;
    public string RootKey => "Progress";

    public void UpdateSaveData(JSON data)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    public static void AdvanceLevel() => Instance._level++;

}

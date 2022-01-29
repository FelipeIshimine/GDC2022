using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class StatsManager : RuntimeScriptableSingleton<StatsManager>
{
    private readonly List<StatTypeToReference> _references = new List<StatTypeToReference>();
    private static Dictionary<string, AssetReferenceGameObject> _idToRef;

    private static Dictionary<string, AssetReferenceGameObject> IdToRef
    {
        get
        {
            if (_idToRef == null) InitializeDictionary();
            return _idToRef;
        }
    }

    [ListDrawerSettings(HideAddButton = true, CustomRemoveIndexFunction = nameof(RemoveStatType))] public List<StatType> stats = new List<StatType>();


    [SerializeField] private StatsPreset playerDefaultStats;
    
    [Title("Attributes")]
    [SerializeField,InlineEditor] private StatType speed;
    public static StatType Speed => Instance.speed;
    
    [SerializeField,InlineEditor] private StatType attack;
    public static StatType Attack => Instance.attack;
    
    [SerializeField,InlineEditor] private StatType actionPoints;
    public static StatType ActionPoints => Instance.actionPoints;

    [SerializeField,InlineEditor] private StatType defense;
    public static StatType Defense => Instance.defense;

    [SerializeField,InlineEditor] private StatType health;
    public static StatType Health => Instance.health;

    [SerializeField,InlineEditor] private StatType maxHealth;
    public static StatType MaxHealth => Instance.maxHealth;

    [SerializeField,InlineEditor] private StatType maxActionPoints;
    public static StatType MaxActionPoints => Instance.maxActionPoints;
    
    

    private static void InitializeDictionary()
    {
        _idToRef = new Dictionary<string, AssetReferenceGameObject>();
        foreach (StatTypeToReference instanceReference in Instance._references)
            _idToRef.Add(instanceReference.type.Id, instanceReference.reference);
    }

    public static AssetReferenceGameObject GetReferenceFromId(string id) => IdToRef[id];
    
    
    public void RemoveStatType(int index)
    {
        var tag = stats[index];
        stats.RemoveAt(index);
        DestroyImmediate(tag, true);
        #if UNITY_EDITOR
        UnityEditor.AssetDatabase.SaveAssets();
        #endif
    }

    
#if UNITY_EDITOR
    
    [Button]
    public static StatType CreateNewStat(string nTagName)
    {
        return Instance.FindOrCreateTag(nTagName);
    }

    public StatType FindOrCreateTag(string tagName)
    {
        StatType tag = stats.Find(x => x.name == tagName);
        return (tag == null)?CreateTag(tagName): tag;
    }

    private StatType CreateTag(string tagName)
    {
        StatType nTag = CreateInstance<StatType>();
        nTag.name = tagName;
        UnityEditor.AssetDatabase.AddObjectToAsset(nTag, this);
        stats.Add(nTag);
        return nTag;
    }

    
    [Button]
    public void ScanForSupplyTypes()
    {
        HashSet<StatType> alreadyAddedTypes = new HashSet<StatType>();

        foreach (var reference in _references)
            alreadyAddedTypes.Add(reference.type);

        foreach (StatType attributeType in UnityEditorUtils.FindAssetsByType<StatType>())
        {
            if(alreadyAddedTypes.Contains(attributeType))
                continue;
            
            _references.Add(new StatTypeToReference(attributeType, new AssetReferenceGameObject(string.Empty)));
        }
    }
#endif
    public static StatType GetTypeFromId(string id) => Instance._references.Find(x => x.type.Id == id).type;

    public static Dictionary<string, int> CreateDefaultStats() => Instance.playerDefaultStats.Create();
}
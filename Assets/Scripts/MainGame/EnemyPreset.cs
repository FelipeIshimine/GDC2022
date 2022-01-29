using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Create EnemyPreset", fileName = "EnemyPreset", order = 0)]
public class EnemyPreset : ScriptableObject
{
    [MinMaxSlider(0,5,true)]public Vector2Int tierRange = new Vector2Int(0,5);
    public StatsPreset stats;
    [SerializeReference] public List<BattleEffect> effects = new List<BattleEffect>(); 
    public AssetReferenceGameObject prefabReference;
}
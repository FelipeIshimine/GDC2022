using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create CoinPreset", fileName = "CoinPreset", order = 0)]
public class CoinPreset : ScriptableObject
{
    [SerializeReference] public Coin coin;
    
#if UNITY_EDITOR
    [Button]
    public void RenameFromEffects()
    {
        string nName = $"{coin.tier}_{coin.headEffect.GetType().Name.Replace("Effect",string.Empty)}_{coin.tailEffect.GetType().Name.Replace("Effect",string.Empty)}";
        UnityEditor.AssetDatabase.RenameAsset(UnityEditor.AssetDatabase.GetAssetPath(this), nName);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }
#endif
}
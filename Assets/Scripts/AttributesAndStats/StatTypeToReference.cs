using Sirenix.OdinInspector;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class StatTypeToReference
{
    [HorizontalGroup,HideLabel] public StatType type;
    [HorizontalGroup,HideLabel] public AssetReferenceGameObject reference;
    public StatTypeToReference() { }
    public StatTypeToReference(StatType type, AssetReferenceGameObject reference)
    {
        this.type = type;
        this.reference = reference;
    }
}
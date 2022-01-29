using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class CoinPresetPair
{
    [Required]public CoinPreset coinPreset;
    [Min(1)]public int amount =1;
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create DeckPreset", fileName = "DeckPreset", order = 0)]
public class DeckPreset : ScriptableObject
{
    public List<CoinPresetPair> coinPairs = new List<CoinPresetPair>();
}
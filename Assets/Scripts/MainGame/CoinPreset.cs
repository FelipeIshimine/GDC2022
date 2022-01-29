using UnityEngine;

[CreateAssetMenu(menuName = "Create CoinPreset", fileName = "CoinPreset", order = 0)]
public class CoinPreset : ScriptableObject
{
    [SerializeReference] public Coin coin;
}
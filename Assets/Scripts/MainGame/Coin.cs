using UnityEngine;

[System.Serializable]
public class Coin
{
    [Range(1,5)] public int tier;
    [SerializeReference] public CoinEffect headEffect;
    [SerializeReference] public CoinEffect tailEffect;
}
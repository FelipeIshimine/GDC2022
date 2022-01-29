using UnityEngine;

[System.Serializable]
public class Coin
{
    [Range(1,5)] public int tier;
    
    [SerializeReference] public BattleEffect headEffect;
    [SerializeReference] public BattleEffect tailEffect;

    
}
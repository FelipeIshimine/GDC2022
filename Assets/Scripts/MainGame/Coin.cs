using UnityEngine;

[System.Serializable]
public class Coin
{
    [Range(0,4)] public int tier;
    
    [SerializeReference] public BattleEffect headEffect;
    [SerializeReference] public BattleEffect tailEffect;
    
}
using UnityEngine;

[System.Serializable]
public class Coin
{
    [Range(0,4)] public int tier;
    
    [SerializeReference] public BattleEffect headEffect;
    [SerializeReference] public BattleEffect tailEffect;

    public int ValueFromTier => CoinManager.GetValueForTier(tier);
    public Sprite HeadIcon => headEffect.Icon;
    public Sprite TailIcon => tailEffect.Icon;
    public Texture HeadCoinIcon => headEffect.CoinTexture;
    public Texture TailCoinIcon => tailEffect.CoinTexture;

    public string HeadDescription() => headEffect.Description(this);
    public string TailDescription() => tailEffect.Description(this);
}
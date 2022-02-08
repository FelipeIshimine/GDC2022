using UnityEngine;

public class BlockEffect : BattleEffect
{
    public override Sprite Icon => IconManager.Instance.defenseIcon;
    public override Texture CoinTexture => IconManager.Instance.defenseCoinTexture;
    public override string Description(Coin coin) => $"You put up your defense. You will block {coin.ValueFromTier} points of damage.";
    
    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        Debug.Log(tier);
        source.Modify(StatsManager.Defense, CoinManager.TierValues[tier]);
    }
}
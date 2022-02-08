using UnityEngine;

public class EvadeEffect : BattleEffect
{
    public override Sprite Icon => IconManager.Instance.evadeIcon;
    public override Texture CoinTexture => IconManager.Instance.evadeCoinTexture;

    public override string Description(Coin coin) => $"Your prepare to dodge the next attack. {coin.ValueFromTier} probability of success";

    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        source.Modify(StatsManager.Speed, CoinManager.TierValues[tier]);
    }
}
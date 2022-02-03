using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CoinDeck 
{
    public List<Coin> coins = new List<Coin>();
}

[System.Serializable]
public abstract class BattleEffect
{
    public abstract Sprite Icon { get; }
    public abstract Texture CoinTexture { get; }

    public abstract string Description(Coin coin);

    public abstract void Apply(BattleUnit source, BattleUnit target, int tier);
}

public class AttackEffect : BattleEffect
{
    public override Sprite Icon => IconManager.Instance.attackIcon;
    public override Texture CoinTexture => IconManager.Instance.attackCoinTexture;

    public override string Description(Coin coin) => $"You ready your attack. You will deal {coin.ValueFromTier} points of damage to the enemy.";

    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        var attackAmount = source.Attack + CoinManager.TierValues[tier];
        var defenceAmount = target.Defense;

        int healthDamage = Mathf.Min(defenceAmount - attackAmount, 0);
        int armorDamage = Mathf.Min(defenceAmount, attackAmount);
        
        target.Modify(StatsManager.Defense, -armorDamage);
        target.Modify(StatsManager.Health, healthDamage);
    }
}

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
/*
public class InvestmentEffect : BattleEffect
{
    
    public override string Description()
    {
        throw new System.NotImplementedException();
    }

    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        throw new System.NotImplementedException();
    }
}*/
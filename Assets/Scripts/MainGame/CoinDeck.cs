using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CoinDeck 
{
    public List<Coin> _coins = new List<Coin>();
}

[System.Serializable]
public abstract class BattleEffect
{
    public abstract string Description();

    public abstract void Apply(BattleUnit source, BattleUnit target, int tier);
}

public class AttackEffect : BattleEffect
{
    public override string Description()
    {
        throw new System.NotImplementedException();
    }

    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        var attackAmount = source.Attack + CoinManager.TierValues[tier];
        var defenceAmount = target.Defense;

        int damage = attackAmount - defenceAmount;

        target.Modify(StatsManager.Defense, attackAmount);
        target.Modify(StatsManager.Health, damage);
    }
}

public class BlockEffect : BattleEffect
{
    public override string Description()
    {
        throw new System.NotImplementedException();
    }
    
    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        source.Modify(StatsManager.Defense, CoinManager.TierValues[tier]);
    }
}

public class EvadeEffect : BattleEffect
{
    public override string Description()
    {
        throw new System.NotImplementedException();
    }

    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        source.Modify(StatsManager.Speed, CoinManager.TierValues[tier]);
    }
}

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
}
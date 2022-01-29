using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CoinDeck 
{
    public List<Coin> _coins = new List<Coin>();
}

[System.Serializable]
public abstract class CoinEffect
{
    public abstract void Execute();
    public abstract string Description();
}

public class AttackEffect : CoinEffect
{
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override string Description()
    {
        throw new System.NotImplementedException();
    }
}

public class BlockEffect : CoinEffect
{
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override string Description()
    {
        throw new System.NotImplementedException();
    }
}

public class EvadeEffect : CoinEffect
{
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override string Description()
    {
        throw new System.NotImplementedException();
    }
}

public class InvestmentEffect : CoinEffect
{
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override string Description()
    {
        throw new System.NotImplementedException();
    }
}
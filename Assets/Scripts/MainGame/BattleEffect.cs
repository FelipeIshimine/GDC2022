using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public abstract class BattleEffect
{
    public abstract Sprite Icon { get; }
    public abstract Texture CoinTexture { get; }

    public abstract string Description(Coin coin);

    public abstract void Apply(BattleUnit source, BattleUnit target, int tier);
    public virtual Task ApplyAsync(BattleUnit source, BattleUnit target, int tier)
    {
        Apply(source,target, tier);
        return Task.CompletedTask;
    }
}
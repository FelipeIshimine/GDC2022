using System.Threading.Tasks;
using UnityEngine;

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

    public override async Task ApplyAsync(BattleUnit source, BattleUnit target, int tier)
    {
        if (source is Enemy)
        {
            bool isReady = false;
            void SetReady() => isReady = true;
            EnemyEntity.Instance.Attack(()=> Apply(source,target,tier) ,SetReady);
            while (!isReady)
                await Task.Yield();
        }
        else
            await base.ApplyAsync(source, target, tier);
    }

}
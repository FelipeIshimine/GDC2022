using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackEffect : BattleEffect
{
    public override Sprite Icon => IconManager.Instance.attackIcon;
    public override Texture CoinTexture => IconManager.Instance.attackCoinTexture;

    public override string Description(Coin coin) => $"You ready your attack. You will deal {coin.ValueFromTier} points of damage to the enemy.";

    public override void Apply(BattleUnit source, BattleUnit target, int tier)
    {
        var attackAmount = source.Attack + CoinManager.TierValues[tier];
        var defenceAmount = target.Defense;

        var value = Random.Range(0, 100);
        bool hit = value > target.Speed;

        if (hit)
        {
            Debug.Log("HIT");
            int healthDamage = Mathf.Min(defenceAmount - attackAmount, 0);
            int armorDamage = Mathf.Min(defenceAmount, attackAmount);
        
            target.Modify(StatsManager.Defense, -armorDamage);
            target.Modify(StatsManager.Health, healthDamage); 
        }
        else
        {
            Debug.Log("DODGE");
            target.Modify(StatsManager.Health, 0);
        } 
    }

    public override void ApplyWithAnimation(BattleUnit source, BattleUnit target, int tier, Action callback)
    {
        if (source is Enemy)
            EnemyEntity.Instance.Attack(()=> Apply(source,target,tier), callback);
        else
            base.ApplyWithAnimation(source, target, tier, callback);
    }

}
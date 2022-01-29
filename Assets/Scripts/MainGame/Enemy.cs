using System.Collections.Generic;
using UnityEngine;


public class Enemy : BattleUnit
{
    public List<BattleEffect> BattleEffects;
    public EnemyIA AI;
    public BattleEffect SelectedEffect;
    public Vector2Int TierRange;
    public Enemy(EnemyPreset preset) : base(preset.stats)
    {
        BattleEffects = preset.effects;
        AI = new EnemyIA(this);
        TierRange = preset.tierRange;
    }

    public void SelectEffect() => SelectedEffect = AI.NextAction();

    public int GetRandomTier() => TierRange.GetRandom();
}
using System;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : BattleUnit
{
    public event Action<BattleEffect> OnEffectSelected;
    
    public List<BattleEffect> BattleEffects;
    public EnemyIA AI;
    public BattleEffect SelectedEffect;
    public Vector2Int TierRange;
    public int SelectedTier;
    public Sprite Sprite;

    public Enemy(EnemyPreset preset) : base(preset.stats)
    {
        Sprite = preset.sprite;
        BattleEffects = preset.effects;
        AI = new EnemyIA(this);
        TierRange = preset.tierRange;
    }

    public void SelectEffect()
    {
        SelectedTier = TierRange.GetRandom();
        SelectedEffect = AI.NextAction();
        OnEffectSelected?.Invoke(SelectedEffect);
    }

    public void ClearEffect()
    {
        SelectedEffect = null;
        OnEffectSelected?.Invoke(null);
    }
}
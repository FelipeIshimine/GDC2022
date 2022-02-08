using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEntity : BaseMonoSingleton<EnemyEntity>
{
    public SpriteRenderer enemyRenderer;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    private Enemy _enemy;
    
    public Image effectIcon;
    
    public TextMeshProUGUI effectText;
    public EnemyShield shield;
    public EnemyAttack attack;
    
    public BlockUI blockUI;

    private bool IsOnTurn => _enemy.IsOnTurn;
    private IReadOnlyDictionary<string, int> Stats => _enemy.Stats;

    private int GetStat(StatType type)
    {
        Stats.TryGetValue(type.Id, out int value);
        return value;
    }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.OnStatsModify += StatsModified;
        _enemy.OnEffectSelected += EffectSelected;
        enemyRenderer.sprite = enemy.Sprite;
        StatsModified(StatsManager.Health.Id,0);
    }

    private void Terminate()
    {
        _enemy.OnEffectSelected -= EffectSelected;
        _enemy.OnStatsModify -= StatsModified;
    }

    private void StatsModified(string id, int amount)
    {
        if (id == StatsManager.Health.Id)
        {
            healthText.text = $"{_enemy.Health}/{_enemy.MaxHealth}";
            healthSlider.value = (float)_enemy.Health / _enemy.MaxHealth;
        }
        else if (id == StatsManager.Defense.Id)
        {
            if (IsOnTurn)
            {
                int defenseValue = Stats[StatsManager.Defense.Id];
                if(defenseValue > 0)
                {
                    if (amount < 0)
                        shield.PlayBlockAnimation();
                    else if (amount > 0)
                        shield.PlayAppearAnimation();
                }
                else if(amount<0)
                {
                    shield.PlayBreakAnimation();
                }

            }
            else if (Stats[StatsManager.Defense.Id] == 0 && amount < 0)
                shield.PlayDisappearAnimation();
        }

        blockUI.SetValue(GetStat(StatsManager.Defense));
    }

    private void EffectSelected(BattleEffect battleEffect)
    {
        if (battleEffect == null) return;
        
        effectIcon.sprite = battleEffect.Icon;
        effectText.text = CoinManager.TierValues[_enemy.SelectedTier].ToString();
    }

    public void Attack(Action applyCallback, Action callback) => attack.Play(applyCallback, callback);
}
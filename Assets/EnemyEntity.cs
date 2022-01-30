using System.Collections;
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
    }
    
    private void EffectSelected(BattleEffect battleEffect)
    {
        if (battleEffect != null)
        {
            effectIcon.sprite = battleEffect.Icon;
            effectText.text = CoinManager.TierValues[_enemy.SelectedTier].ToString();
        }
    }
}

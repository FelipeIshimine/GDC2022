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
    
    public SpriteRenderer defendShield;
    public float defenseShieldAnimDuration = .4f;
    public AnimationCurve colorCurve;
    public TextMeshProUGUI effectText;
    public EnemyShield enemyShield;
    
    private IEnumerator _routine;
    
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

        if (id == StatsManager.Defense.Id && amount < 0)
            ShieldAnimation();
    }

    private void ShieldAnimation()
    {
        this.PlayCoroutine(ref _routine, ShieldAnimationRoutine);
    }

    private void EffectSelected(BattleEffect battleEffect)
    {
        if (battleEffect != null)
        {
            effectIcon.sprite = battleEffect.Icon;
            effectText.text = CoinManager.TierValues[_enemy.SelectedTier].ToString();
        }
    }

    private IEnumerator ShieldAnimationRoutine()
    {
        float t = 0;
        defendShield.gameObject.SetActive(true);
        do
        {
            t += Time.deltaTime / defenseShieldAnimDuration;
            defendShield.color = Color.Lerp(Color.white, Color.clear, colorCurve.Evaluate(t));
            yield return null;
        } while (t<1);
        defendShield.gameObject.SetActive(false);
    }
}

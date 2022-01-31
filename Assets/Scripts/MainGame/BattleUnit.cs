using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleUnit
{
    public Action<string,int> OnStatsModify;
    public Dictionary<string, int> Stats;

    public bool IsDead() => Health <= 0;

    #region StatGetters
    public int ActionPoints => Get(StatsManager.ActionPoints.Id);
    public int Health => Get(StatsManager.Health.Id);
    public int Attack => Get(StatsManager.Attack.Id);
    public int Defense => Get(StatsManager.Defense.Id);
    public int HandSize => Get(StatsManager.HandSize.Id);
    public int MaxActionPoints => Get(StatsManager.MaxActionPoints.Id);
    public int MaxHealth => Get(StatsManager.MaxHealth.Id);

    #endregion

    public BattleUnit(StatsPreset statsPreset)
    {
        Stats = statsPreset.Create();
    }

    public BattleUnit(Dictionary<string,int> playerDataStats)
    {
        Stats = new Dictionary<string, int>(playerDataStats);
        Stats[StatsManager.Health.Id] = Stats[StatsManager.MaxHealth.Id];
    }

    private int Get(string id) => Stats.TryGetValue(id, out int amount) ? amount : 0;

    public void Modify(StatType stat, int amount) => Modify(stat.Id, amount);

    public void Modify(string id, int amount)
    {
        Debug.Log($"Modify {id} {Get(id)}>{Get(id)+amount}");
        Stats[id] = Get(id) + amount;
        OnStatsModify?.Invoke(id, amount);
    }

    public void Set(StatType statType, int amount)
    {
        var oldValue = Get(statType.Id);
        Stats[statType.Id] = amount;
        
        OnStatsModify.Invoke(statType.Id, amount - oldValue);
    }
}
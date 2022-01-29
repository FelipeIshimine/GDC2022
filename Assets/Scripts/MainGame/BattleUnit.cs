using System;
using System.Collections.Generic;

public class BattleUnit
{
    public Action<string,int> OnModify;
    public Dictionary<string, int> Stats;

    public bool IsDead() => Health == 0;

    #region StatGetters
    public int ActionPoints => Stats[StatsManager.ActionPoints.Id];
    public int Health => Stats[StatsManager.Health.Id];
    public int Attack => Stats[StatsManager.Attack.Id];
    public int Defense => Stats[StatsManager.Defense.Id];
    public int HandSize => Stats[StatsManager.HandSize.Id];

    #endregion

    public BattleUnit(StatsPreset statsPreset)
    {
        Stats = statsPreset.Create();
    }

    public BattleUnit(Dictionary<string,int> playerDataStats)
    {
        Stats = new Dictionary<string, int>(playerDataStats);
    }

    public void Modify(StatType stat, int amount) => Modify(stat.Id, amount);

    public void Modify(string id, int amount)
    {   
        OnModify?.Invoke(id, amount);
    }
}
using System.Collections.Generic;

public class PlayerData
{
    public int BattleId;
    public List<Coin> Deck;
    public Dictionary<string, int> Stats;
    
    public int ActionPoints => Stats[StatsManager.ActionPoints.Id];
    public int MaxActionPoints => Stats[StatsManager.MaxActionPoints.Id];
    public int Health => Stats[StatsManager.Health.Id];
    public int MaxHealth => Stats[StatsManager.MaxHealth.Id];
    public int Attack => Stats[StatsManager.Attack.Id];
    public int Defense => Stats[StatsManager.Defense.Id];
    public int Speed => Stats[StatsManager.Speed.Id];
}
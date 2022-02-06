using System.Collections.Generic;

public class PlayerData
{
    public int LevelId;
    public readonly List<Coin> Deck;
    public readonly Dictionary<string,int> Stats;

    public PlayerData(int levelId, List<Coin> deck, Dictionary<string, int> stats)
    {
        Stats = stats;
        Deck = deck;
        LevelId = levelId;
    }

    public int ActionPoints => Stats[StatsManager.ActionPoints.Id];
    public int MaxActionPoints => Stats[StatsManager.MaxActionPoints.Id];
    public int Health => Stats[StatsManager.Health.Id];
    public int Attack => Stats[StatsManager.Attack.Id];
    public int Defense => Stats[StatsManager.Defense.Id];
    public int Speed => Stats[StatsManager.Speed.Id];
}
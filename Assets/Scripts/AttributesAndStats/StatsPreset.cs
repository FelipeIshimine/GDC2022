using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create StatsPreset", fileName = "StatsPreset", order = 0)]
public class StatsPreset : ScriptableObject
{
    [InfoBox("Duplicated Stats", InfoMessageType.Error, nameof(HasDuplicatedTypes))]public List<StatPair> values = new List<StatPair>();

    private bool HasDuplicatedTypes()
    {
        HashSet<StatType> statTypes = new HashSet<StatType>();
        for (int i = values.Count - 1; i >= 0; i--)
        {
            var value = values[i];
            if (statTypes.Contains(value.type))
                return true;
        }
        return false;
    }

    public Dictionary<string,int> Create()
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        foreach (StatPair statPair in values)
            dictionary.Add(statPair.type.Id, statPair.amount);

        dictionary[StatsManager.Health.Id] = dictionary[StatsManager.MaxHealth.Id];
        return dictionary;
    }
}
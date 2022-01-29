using System.Collections.Generic;
using UnityEngine;

namespace CharacterStats
{
    [System.Serializable]
    public class FlatModifier : BaseModifier
    {
        [SerializeField, ScriptableObjectDropdownString(typeof(StatType))] private string id;
        [SerializeField] private int amount;

        public FlatModifier()
        {
        }

        public FlatModifier(object source, string id, int amount) :base(source)
        {
            this.id = id;
            this.amount = amount;
        }

        public override (string id, int value)[] CalculateOnly(IReadOnlyDictionary<string, int> values) => new (string id, int nValues)[] {(id,  values[id] + amount)};
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStats
{
    [System.Serializable]
    public class PercentageModifier : BaseModifier
    {
        [SerializeField,ScriptableObjectDropdownString(typeof(StatType))] private string id;
        [SerializeField] private float percentage;
        
        public PercentageModifier()
        {
        }

        public PercentageModifier(object source, string id, float percentage) : base(source)
        {
            this.id = id;
            this.percentage = percentage;
        }

        public override (string id, int value)[] CalculateOnly(IReadOnlyDictionary<string, int> values) => new (string id, int nValues)[] {(id,  Mathf.RoundToInt(values[id] * percentage))};
    }
}
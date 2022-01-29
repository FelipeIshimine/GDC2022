using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CharacterStats
{
    [System.Serializable,InlineProperty]
    public abstract class BaseModifier : IComparer<BaseModifier>
    {
        public object Source { get; private set; }
        public readonly int Priority;

        protected BaseModifier()
        {
        }

        protected BaseModifier(object source, int priority = -1)
        {
            Source = source;
            if (priority >= 0)
                Priority = priority;
            else
                Priority = 0;
        }

        public void OverrideSource(object nSource) => Source = nSource;
        
        /// <summary>
        /// Calcula y luego aplica la modificacion que le realizaria al diccionario otorgado
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public (string id, int value)[] CalculateAndApply(Dictionary<string, int> values)
        {
            Debug.Log($"Apply Modifier: {this}");
            var newValues = CalculateOnly(values);
            foreach ((string statId, int nValue) in newValues)
                values[statId] = nValue;
            return newValues;
        }
        
        /// <summary>
        /// Calcula la modificacion que le realizaria al diccionario otorgado
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public abstract (string id, int value)[] CalculateOnly(IReadOnlyDictionary<string, int> values);

        public int Compare(BaseModifier x, BaseModifier y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.Priority.CompareTo(y.Priority);
        }
    }
}
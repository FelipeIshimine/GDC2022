using System;
using System.Collections.Generic;
using System.Linq;
using CharacterStats;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    public event Action<BaseModifier> OnEquipModifier;
    public event Action<BaseModifier> OnUnequipModifier;

    public event Action OnStatsChange;

    public int this[StatType value] => Get(value.Id);
    public int this[string value] => Get(value);

    #region Attributes

    private readonly Dictionary<string, int> _baseAttributes = new Dictionary<string, int>();
    public IReadOnlyDictionary<string, int> BaseAttributes => _baseAttributes;

    #endregion

    #region Modifiers

    private readonly Dictionary<object, List<BaseModifier>> _sourceToModifiers =
        new Dictionary<object, List<BaseModifier>>();

    private readonly List<BaseModifier> _modifiers = new List<BaseModifier>();
    public IReadOnlyList<BaseModifier> Modifiers => _modifiers;

    #endregion

    #region Stats

    [ShowInInspector, ReadOnly] private Dictionary<string, int> _stats = new Dictionary<string, int>();
    public IReadOnlyDictionary<string, int> Stats => _stats;

    #endregion

    #region Initialization

    public void Initialize(List<KeyValuePair<string, int>> valueTuples) =>
        Initialize(valueTuples.ConvertAll(x => (x.Key, x.Value)));

    public void Initialize(List<(StatType key, int value)> valueTuples) =>
        Initialize(valueTuples.ConvertAll(x => (x.key.Id, x.value)));

    public void Initialize(List<(string key, int value)> valueTuples)
    {
        _baseAttributes.Clear();
        foreach ((string key, int value) in valueTuples)
            _baseAttributes.Add(key, value);

        _stats.Clear();
        _stats = new Dictionary<string, int>(_baseAttributes);
    }

    public void Terminate()
    {
    }

    #endregion

    private void Recalculate()
    {
        _stats = new Dictionary<string, int>(_baseAttributes);
        for (var index = 0; index < _modifiers.Count; index++)
            _modifiers[index].CalculateAndApply(_stats);
    }

    public void EquipModifier(BaseModifier modifier) => EquipModifiers(new[] { modifier });

    public void EquipModifiers(IEnumerable<BaseModifier> modifiers)
    {
        //Metemos los valores en un arreglo porque se va a volver a utilizar, y los IEnumerable, devuelven valoress solamente
        var baseModifiers = modifiers as BaseModifier[] ?? modifiers.ToArray();
        foreach (var modifier in baseModifiers)
        {
            if (!_sourceToModifiers.ContainsKey(modifier.Source))
                _sourceToModifiers.Add(modifier.Source, new List<BaseModifier>());

            _sourceToModifiers[modifier.Source].Add(modifier);
            _modifiers.Add(modifier);
        }

        _modifiers.Sort();

        Recalculate();

        foreach (var modifier in baseModifiers)
            OnEquipModifier?.Invoke(modifier);

        OnStatsChange?.Invoke();
    }

    public void UnequipModifier(BaseModifier modifier) => UnequipModifiers(new[] { modifier });

    public void UnequipModifiers(IEnumerable<BaseModifier> modifiers)
    {
        var baseModifiers = modifiers as BaseModifier[] ?? modifiers.ToArray();
        foreach (var modifier in baseModifiers)
        {
            var list = _sourceToModifiers[modifier.Source];
            list.Remove(modifier);
            if (list.Count == 0) _sourceToModifiers.Remove(modifier.Source);
            _modifiers.Remove(modifier);
        }

        _modifiers.Sort();

        Recalculate();

        foreach (var modifier in baseModifiers)
            OnUnequipModifier?.Invoke(modifier);
        OnStatsChange?.Invoke();
    }

    public void UnequipAllModifierFrom(object source) =>
        UnequipModifiers(new List<BaseModifier>(_sourceToModifiers[source]).ToArray());

    private int Get(string key) => _stats.ContainsKey(key) ? _stats[key] : 0;
}


[System.Serializable]
public class StatPair
{
    [Required][HorizontalGroup(Width = .6f),HideLabel] public StatType type;
    [HorizontalGroup,HideLabel]public int amount;
}


using System;
using CharacterStats;
using UnityEngine;

[System.Serializable]
public class ModifierTimer
{
    private readonly BaseModifier _modifier;
    private readonly StatsComponent _statsComponent;
    private readonly Action _onDoneOrAbort;
    private AutoTimer _timer;
    
    public ModifierTimer(float duration, BaseModifier modifier, StatsComponent statsComponent, Action onDoneOrAbort)
    {
        _modifier = modifier;
        _statsComponent = statsComponent;
        _onDoneOrAbort = onDoneOrAbort;
        _timer = new AutoTimer(duration, false, DeltaTimeType.deltaTime, Unequip);
        _statsComponent.OnUnequipModifier += OnStatsUnequipModifier; 
    }

    public void Play() => _timer.Play();
    public void Pause() => _timer.Pause();
    
    private void OnStatsUnequipModifier(BaseModifier modifier)
    {
        if (modifier != _modifier) return;
        Debug.Log("OnStatsUnequipModifier");

        //Abort
        _statsComponent.OnUnequipModifier -= OnStatsUnequipModifier; 
        _timer.Abort();
        _onDoneOrAbort?.Invoke();
    }

    private void Unequip()
    {
        Debug.Log("Unequip");
        _statsComponent.OnUnequipModifier -= OnStatsUnequipModifier; 
        _statsComponent.UnequipModifier(_modifier);
        _onDoneOrAbort?.Invoke();
    }
}
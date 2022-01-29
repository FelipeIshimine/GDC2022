using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsComponent))]
public class BattleUnitObject : MonoBehaviour
{
    [SerializeReference] private StatsComponent statsComponent;
    

    
    public void Initialize()
    {
    }

    public void Terminate()
    {
        
    }

    private void OnValidate()
    {
        if (statsComponent) statsComponent = GetComponent<StatsComponent>();
    }
}

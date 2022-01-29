using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterStats;

public class PickeableAttributeModifier : MonoBehaviour
{
    public Renderer render;
    [SerializeReference] public BaseModifier modifier;
    public float duration = 20;
    
    private void OnTriggerEnter(Collider other)
    {
        if(!enabled)
            return;
        if (other.gameObject.TryGetComponent(out StatsComponent statsComponent))
        {
            modifier.OverrideSource(this);
            statsComponent.EquipModifier(modifier);
            ModifierTimer modifierTimer = new ModifierTimer(duration, modifier, statsComponent, Done);
            modifierTimer.Play();
            render.enabled = false;
            transform.SetParent(other.gameObject.transform);
            transform.localPosition = Vector3.zero;
            enabled = false;
        }
    }

    private void Done()
    {
        Destroy(gameObject);
    }
}
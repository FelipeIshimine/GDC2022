using UnityEngine;

public class StatType : ScriptableObject
{
    public string Id => name;
    
    public static implicit operator string(StatType type) => type.Id;
}
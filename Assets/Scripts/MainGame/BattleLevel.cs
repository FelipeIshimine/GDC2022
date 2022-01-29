using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Create BattleLevel", fileName = "BattleLevel", order = 0)]
public class BattleLevel : ScriptableObject
{
    [InlineEditor] public EnemyPreset enemyPreset;
}

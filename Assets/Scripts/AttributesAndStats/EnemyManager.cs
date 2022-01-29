using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : RuntimeScriptableSingleton<EnemyManager>
{
    //public Dictionary<string, EnemyStats> enemyStats;
    public List<EnemyManagerInfo> enemyStats;

    /// <summary>
    /// devuelve el EnemyState con el nombre que coincida
    /// </summary>
    /// <param name="nameID"></param>
    /// <returns></returns>
    public EnemyStats GetStat(MonoBehaviour enemy)
    {
        //foreach (var item in enemyStats)
        //{
        //    Debug.Log($"Pedido: {enemy}, Item: {item.Enemy.GetComponent}");
        //    //if (item.Enemy == enemy)
        //    //    return item.stats;
        //}
        return null;
    }
}
[System.Serializable]
public struct EnemyManagerInfo
{
    public GameObject Enemy;
    public EnemyStats stats;
    public GameObject drop;
}
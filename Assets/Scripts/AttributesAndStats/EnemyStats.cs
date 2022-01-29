using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyStats"),System.Serializable]
public class EnemyStats : ScriptableObject
{
    public int life;
    public int damage;
    public float speed;
    public GameObject drop;
    public GameObject dieParticle;
}

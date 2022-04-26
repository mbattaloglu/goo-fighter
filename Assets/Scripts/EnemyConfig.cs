using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Config", fileName ="NewEnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public float health;
    public float speed;
}

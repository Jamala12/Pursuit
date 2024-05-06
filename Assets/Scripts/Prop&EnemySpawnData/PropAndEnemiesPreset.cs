using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PropsAndEnemiesPreset", menuName = "Dungeon/Props and Enemies Preset")]
public class PropsAndEnemiesPreset : ScriptableObject
{
    public GameObject[] propPrefabs;
    public List<EnemyGroup> enemyGroups;
    public int maxProps;
    public float minDistanceFromStart;
}

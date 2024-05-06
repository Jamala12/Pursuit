using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Group", menuName = "Enemy/Enemy Group")]
public class EnemyGroup : ScriptableObject
{
    public GameObject[] enemyTypes;
    public int groupSize;
    public string formation;
}
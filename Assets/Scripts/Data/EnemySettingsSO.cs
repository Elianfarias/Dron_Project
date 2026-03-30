using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Enemy")]
public class EnemySettingsSO : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float obstacleDetectionRadius = 2f;
    [SerializeField] private LayerMask obstacleLayer;

    public int Damage { get { return damage; } }
    public float WaitTime { get { return waitTime; } }
    public float ObstacleDetectionRadius { get { return obstacleDetectionRadius; } }
    public LayerMask ObstacleLayer { get { return obstacleLayer; } }
}

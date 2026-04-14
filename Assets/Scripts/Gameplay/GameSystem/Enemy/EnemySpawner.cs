using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.GameSystem.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private LevelConfigSO levelConfig;
        [SerializeField] private Transform[] spawnPoints;

        private readonly List<GameObject> spawnedEnemies = new();

        private void Start()
        {
            int spawnedCount = SpawnEnemies();
            ScoreManager.Instance.SetEnemyCount(spawnedCount);
        }

        private int SpawnEnemies()
        {
            int level = LevelManager.Instance != null ? LevelManager.Instance.CurrentLevel : 1;
            int enemyCount = levelConfig.GetEnemyCount(level);

            int count = Mathf.Min(enemyCount, spawnPoints.Length);

            for (int i = 0; i < count; i++)
            {
                Transform point = spawnPoints[i];
                GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);
                spawnedEnemies.Add(enemy);
            }

            return count;
        }
    }
}

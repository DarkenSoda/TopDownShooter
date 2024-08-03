using System.Collections;
using UnityEngine;

namespace Game.FullGame
{
    public class WaveManager : MonoBehaviour
    {
        private Transform player;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int startEnemyCount = 3;
        [SerializeField] private int enemyMultiplier = 2;
        [SerializeField] private Vector2 spawnDistanceFromPlayer = new Vector2(5, 8);
        [SerializeField] private float timeBetweenEnemySpawn = 0.2f;

        private int currentWave = 0;
        private int enemyCount = 0;
        private int enemiesSpawnedThisWave;

        private void Awake()
        {
            enemiesSpawnedThisWave = startEnemyCount;

            player = GameObject.FindGameObjectWithTag("Player").transform;

            player.GetComponent<PlayerHealth>().OnDeath += (_, _) => StopAllCoroutines();
        }

        private void Update()
        {
            if (enemyCount == 0)
            {
                currentWave++;

                StopAllCoroutines();

                enemiesSpawnedThisWave = startEnemyCount + (int)Mathf.Pow(enemyMultiplier, currentWave - 1);
                enemyCount = enemiesSpawnedThisWave;
                StartCoroutine(SpawnEnemies());
            }
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < enemiesSpawnedThisWave; i++)
            {
                float randomAngle = Random.Range(0f, 2f * Mathf.PI);
                float distance = Random.Range(spawnDistanceFromPlayer.x, spawnDistanceFromPlayer.y);
                Vector3 spawnPosition = player.position +
                    new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0) * distance;

                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                enemy.GetComponent<Enemy>().OnDeath += (_, _) => enemyCount--;

                yield return new WaitForSeconds(timeBetweenEnemySpawn);
            }
        }
    }
}

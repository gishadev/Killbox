using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gisha.Killbox.NPC
{
    public class WaveManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Transform[] spawnpoints;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemiesPerWave;
        [Header("Timings")]
        [SerializeField] private float timeTillLoseInSeconds;
        [SerializeField] private float minSpawnDelay, maxSpawnDelay;

        List<GameObject> _enemyObjects = new List<GameObject>();
        //  ол-во врагов, которое осталось заспавнить.
        int _enemiesToSpawn;

        private void Start()
        {
            _enemiesToSpawn = enemiesPerWave;

            StartCoroutine(WaveCoroutine());
        }

        private IEnumerator WaveCoroutine()
        {
            Debug.Log("<color=blue>Start of the wave!</color>");
            StartCoroutine(EnemySpawningCoroutine());

            yield return new WaitForSeconds(timeTillLoseInSeconds);
            Debug.Log("<color=red>End of the wave! You lose!</color>");
        }

        private IEnumerator EnemySpawningCoroutine()
        {
            while (_enemiesToSpawn > 0)
            {
                float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSeconds(spawnDelay);

                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var position = spawnpoints[Random.Range(0, spawnpoints.Length)].position;
            _enemyObjects.Add(Instantiate(enemyPrefab, position, Quaternion.identity));

            _enemiesToSpawn--;
        }

        public void OnEnemyDestroy(GameObject enemyObject)
        {
            _enemyObjects.Remove(enemyObject);

            if (_enemiesToSpawn == 0 && _enemyObjects.Count == 0)
                Debug.Log("<color=green>End of the wave! You win!</color>");
        }
    }
}

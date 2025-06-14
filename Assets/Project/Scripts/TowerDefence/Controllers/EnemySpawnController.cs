using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private GameState _gameState;
    private ObjectPool _weakEnemyPool;
    private ObjectPool _midEnemyPool;
    private ObjectPool _heavyEnemyPool;
    private Dictionary<GameObject, ObjectPool> _enemyPools = new Dictionary<GameObject, ObjectPool>();
    [SerializeField] private WaveData _waveData;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _weakEnemyPrefab;
    [SerializeField] private GameObject _midEnemyPrefab;
    [SerializeField] private GameObject _heavyEnemyPrefab;
    [SerializeField] private float _minimumSpawnDelay = 1;
    [SerializeField] private float _maximumSpawnDelay = 3;
    [SerializeField] private float _timeBetweenWaves = 1;
    [SerializeField] private UnityEvent OnWavesEnded;
    private int _waveNumber;
    private void Start() {
        _weakEnemyPool = new ObjectPool(_weakEnemyPrefab);
        _midEnemyPool= new ObjectPool(_midEnemyPrefab);
        _heavyEnemyPool = new ObjectPool(_heavyEnemyPrefab);
        StartCoroutine(CreateNewEnemyWave());
    }
    IEnumerator CreateNewEnemyWave()
    {
        while(_waveNumber < _waveData.Waves.Length && _gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            yield return new WaitForSeconds(_timeBetweenWaves);
            // StartCoroutine(SpawnEnemies(_waveData.Waves[_waveNumber].WeakEnemies, _weakEnemyPrefab));
            // StartCoroutine(SpawnEnemies(_waveData.Waves[_waveNumber].MidEnemies, _midEnemyPrefab));
            // StartCoroutine(SpawnEnemies(_waveData.Waves[_waveNumber].HeavyEnemies, _heavyEnemyPrefab));
            StartCoroutine(SpawnEnemiesInPool(_waveData.Waves[_waveNumber].WeakEnemies, _weakEnemyPool));
            StartCoroutine(SpawnEnemiesInPool(_waveData.Waves[_waveNumber].MidEnemies, _midEnemyPool));
            StartCoroutine(SpawnEnemiesInPool(_waveData.Waves[_waveNumber].HeavyEnemies, _heavyEnemyPool));
            while (_gameState.EnemyCount > 0)
            {
                yield return null;
            }
            _waveNumber++;
        }
        if(!_gameState.GameOver)
            OnWavesEnded?.Invoke();


    }
    IEnumerator SpawnEnemies(int enemyAmount, GameObject enemyPrefab)
    {
        for(int i = 0; i < enemyAmount; i++)
        {
            Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_minimumSpawnDelay, _maximumSpawnDelay));
        }
    }

    IEnumerator SpawnEnemiesInPool(int enemyAmount, ObjectPool objectPool)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            GameObject enemy = objectPool.GetGameObjectFromPool();
            enemy.transform.position = _spawnPoint.position;
            enemy.SetActive(true);
            yield return new WaitForSeconds(Random.Range(_minimumSpawnDelay, _maximumSpawnDelay));
        }
    }
}

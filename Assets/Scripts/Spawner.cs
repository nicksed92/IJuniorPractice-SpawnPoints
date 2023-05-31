using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _enemiesContainer;
    [SerializeField] private float _spawnDelay = 2f;

    private bool _isSpawning = false;
    private int _currentSpawnPointIndex = 0;
    private WaitForSeconds _waitTime;

    private void Start()
    {
        _waitTime = new WaitForSeconds(_spawnDelay);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        _isSpawning = true;

        while (_isSpawning)
        {
            yield return _waitTime;
            Instantiate(_enemy, GetSpawnPosition(), Quaternion.identity, _enemiesContainer);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        if (_currentSpawnPointIndex == _spawnPoints.Count)
            _currentSpawnPointIndex = 0;

        float radius = 1.0f;

        Vector3 randomOffset = Random.insideUnitSphere * radius;
        Vector3 randomPosition = _spawnPoints[_currentSpawnPointIndex++].localPosition +
            new Vector3(randomOffset.x, 0, randomOffset.y);

        return randomPosition;
    }
}
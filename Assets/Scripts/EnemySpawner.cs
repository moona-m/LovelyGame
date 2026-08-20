using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] Tilemap _groundTiles;
    [SerializeField] float _spawnCooldown;
    [SerializeField] float _sCdReductionMultiplier;
    //sCd = spawnCooldown
    float _currentCooldown;
    List<Vector3> _spawnPositions = new();

    void Start()
    {
        SetEnemySpawnPositions();
        InvokeRepeating(nameof(DifficultyIncrease), 1f, 1f);
    }

    void Update()
    {
        HandleEnemySpawner();
    }

    void DifficultyIncrease()
    {
        _spawnCooldown += _sCdReductionMultiplier;
    }

    Vector3 GetRandomLocation()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
    }

    void SpawnEnemyToRandomLocation()
    {
        Instantiate(_enemyPrefab, GetRandomLocation(), Quaternion.identity);
    }
    void HandleEnemySpawner()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown > Time.time)
            return;

        _currentCooldown = Time.time + _spawnCooldown;
        SpawnEnemyToRandomLocation();
    }
    void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            {
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));

            }
        }
    }

}

using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _spawnInterval = 0.5f;
    [SerializeField] private Vector2 _spawnRangeX = new Vector2(-10, 10);
    [SerializeField] private float _spawnHeight = 10;
    [SerializeField] private Vector2 _spawnRangeZ = new Vector2(-10, 10);
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            Vector3 randomSpawnPosition = new Vector3(
                Random.Range(_spawnRangeX.x, _spawnRangeX.y),
                _spawnHeight,
                Random.Range(_spawnRangeZ.x, _spawnRangeZ.y)
            );

            GameObject obj = _objectPool.GetObject();
            obj.transform.position = randomSpawnPosition;

            FallingObject fallingObject = obj.GetComponent<FallingObject>();

            if (fallingObject != null)
            {
                fallingObject.Initialize(_objectPool, _minLifetime, _maxLifetime);
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
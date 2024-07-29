using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _spawnInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        while (true)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 10, Random.Range(-10, 11));
            GameObject cube = _objectPool.GetObject();
            cube.transform.position = randomSpawnPosition;

            CubeBehaviour cubeBehaviour = cube.GetComponent<CubeBehaviour>();
            if (cubeBehaviour != null)
            {
                cubeBehaviour.Initialize(_objectPool);
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}
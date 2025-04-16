using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject _package;

    [SerializeField]
    float _startingSpawnRate = 5.0f;

    private bool _spawningActive;

    // Start is called before the first frame update
    void Start()
    {
        _spawningActive = true;
        Invoke(nameof(SpawnTarget), 0);
    }

    private void DisableSpawning()
    {
        _spawningActive = false;
    }

    void SpawnTarget()
    {
        float randomX = Random.Range(0, 11);
        float randomPositionX = randomX * 5;
        Vector3 randomPosition = new Vector3(randomPositionX, 55, 5);

        GameObject Package = Instantiate(_package, randomPosition, Quaternion.identity);

        Invoke(nameof(SpawnTarget), _startingSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawningActive == false)
        {
            Invoke(nameof(SpawnTarget), 0);
        }
    }
}

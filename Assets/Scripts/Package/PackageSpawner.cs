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

    int GridWidth = 10;
    int GridHeight = 10;
    int PackageSize = 5;

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
        _spawningActive = true;

        float randomX = Random.Range(0, GridWidth + 1);
        float randomPositionX = randomX * PackageSize;
        Vector3 randomPosition = new Vector3(randomPositionX, GridHeight * PackageSize, 5);

        GameObject Package = Instantiate(_package, randomPosition, Quaternion.identity);
        Package.transform.localScale = new Vector3(PackageSize, PackageSize, 10);

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

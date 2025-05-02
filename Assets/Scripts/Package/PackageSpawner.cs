using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PackageSpawner : MonoBehaviour
{
    //[SerializeField]
    //GameObject _brownPackage;
    //[SerializeField]
    //GameObject _greenPackage;zzzz
    //[SerializeField]
    //GameObject _bluePackage;
    //[SerializeField]
    //GameObject _goldPackage;

    [SerializeField]
    float _startingSpawnRate = 5.0f;

    GameObject[] _packages;

    [SerializeField]
    private int _gridWidth = 10;
    [SerializeField]
    private int _gridHeight = 10;
    [SerializeField]
    private int _packageSize = 5;

    private bool _spawningActive;

    // Start is called before the first frame update
    void Start()
    {
        _spawningActive = true;
        Invoke(nameof(SpawnTarget), 0);
    }

    public int GridWidth()
    {
        return _gridWidth;
    }
    public int GridHeight()
    {
        return _gridHeight;
    }
    public int PackageSize()
    {
        return _packageSize;
    }

    private void DisableSpawning()
    {
        _spawningActive = false;
    }

    void SpawnTarget()
    {
        _spawningActive = true;

        float randomX = Random.Range(0, _gridWidth);
        float randomPositionX = randomX * _packageSize;
        Vector3 randomPosition = new Vector3(randomPositionX, _gridHeight * _packageSize, 5);


        int boxChoice = Random.Range(0, 100);
        GameObject boxToSpawn;
        //5% chance for gold
        if (boxChoice < 5)
        {
            boxChoice = 100;
            boxToSpawn = ObjectPool.SharedInstance.goldToPool;
        }
        //15% chance for blue
        else if (boxChoice < 20)
        {
            boxChoice = 50;
            boxToSpawn = ObjectPool.SharedInstance.blueToPool;
        }
        //30% chance for green
        else if (boxChoice < 50)
        {
            boxChoice = 25;
            boxToSpawn = ObjectPool.SharedInstance.greenToPool;
        }
        //50% chance for brown
        else
        {
            boxChoice = 10;
            boxToSpawn = ObjectPool.SharedInstance.brownToPool;
        }

        GameObject Package = ObjectPool.SharedInstance.ActivateAnObject(boxChoice);
        if (Package == null)
        {
            GameObject temp = Instantiate(boxToSpawn);
            temp.SetActive(false);
            ObjectPool.SharedInstance.AddObjectToPool(boxToSpawn);
            Package = temp;
        }

        //Package = ObjectPool.SharedInstance.ActivateAnObject(boxChoice);
        Package.transform.position = randomPosition;
        Package.transform.localScale = new Vector3(_packageSize, _packageSize, 10);


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

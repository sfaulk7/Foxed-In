using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject brownToPool;
    public GameObject greenToPool;
    public GameObject blueToPool;
    public GameObject goldToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool * 4; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject ActivateAnObject(int objectPointWorth)
    {
        for (int i = 0; i < amountToPool * 4; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                continue;
            }

            if (pooledObjects[i].GetComponent<PackageCollisionBehavior>().PackagePointWorth() == objectPointWorth)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }

        return null;
    }

    public int ActiveObjectCount()
    {
        int count = 0;

        for (int i = 0; i < amountToPool * 4; i++)
        {
            if (pooledObjects[i].activeInHierarchy)
            {
                count++;
            }
        }

        return count;
    }

    public GameObject GetActiveObject(int Object)
    {
        return pooledObjects[Object];
    }

    public void AddObjectToPool(GameObject addedObject)
    {
        GameObject temp;
        temp = Instantiate(addedObject);
        temp.SetActive(false);
        pooledObjects.Add(temp);
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for (int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(goldToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);

            temp = Instantiate(blueToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);

            temp = Instantiate(greenToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);

            temp = Instantiate(brownToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }
}
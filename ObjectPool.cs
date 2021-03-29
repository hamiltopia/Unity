//https://learn.unity.com/tutorial/introduction-to-object-pooling-2019-3


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}



//This code goes on the script instantating the gameObject
//GameObject projectile = ObjectPool.SharedInstance.GetPooledObject();

//if (projectile != null)
//{
//    projectile.transform.position = firePoint.transform.position;
//    projectile.transform.rotation = firePoint.transform.rotation;
//    projectile.SetActive(true);
//}


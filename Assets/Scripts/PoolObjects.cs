using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class PoolObjectItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public class PoolObjects : MonoBehaviour
{
    public List<PoolObjectItem> itemsToPool;
    public List<GameObject> pooledObjects;

    private static PoolObjects instance;
    public static PoolObjects Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start ()
    {
        pooledObjects = new List<GameObject>();

        foreach (PoolObjectItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
	
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        foreach (PoolObjectItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }
}

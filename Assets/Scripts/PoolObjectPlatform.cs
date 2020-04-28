using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectPlatform : MonoBehaviour
{
    public List<GameObject> pooledObjectsPlatform;
    public int amountToPool = 2;

    private static PoolObjectPlatform instance;
    public static PoolObjectPlatform Instance { get { return instance; } }

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

    void Start()
    {
        pooledObjectsPlatform = new List<GameObject>();

        for (int g = 0; g < amountToPool; g++)
        {
            for (int i = 0; i < LevelGenerator.Instance.platform.Count; i++)
            {
                    GameObject go = Instantiate(LevelGenerator.Instance.platform[i]);
                    go.SetActive(false);
                    pooledObjectsPlatform.Add(go);
            }
        }
    }

    public void DisablePooledObject()
    {
        for (int i = 0; i < pooledObjectsPlatform.Count; i++)
        {
            if (pooledObjectsPlatform[i].activeSelf)
            {
                pooledObjectsPlatform[i].SetActive(false);
                GameObject disabledGO = pooledObjectsPlatform[i];
                pooledObjectsPlatform.Remove(disabledGO);
                pooledObjectsPlatform.Add(disabledGO);
                return;
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjectsPlatform.Count; i++)
        {
            if (!pooledObjectsPlatform[i].activeSelf)
                return pooledObjectsPlatform[i];
        }
        
        return null;
    }
}
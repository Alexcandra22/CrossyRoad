﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform startPos = null;

    public float delayMin = 2.5f;
    public float delayMax = 5;
    public float speedMin = 1;
    public float speedMax = 3;

    public bool useSpawnPlacement = false;
    public int spawnCountMin = 4;
    public int spawnCountMax = 15;

    private float lastTime = 0;
    private float delayTime = 0;
    private float speed = 0;

    [HideInInspector]
    public GameObject item = null;
    public static GameObject itemForPool = null;
    [HideInInspector]
    public bool goLeft = false;
    [HideInInspector]
    public float spawnLeftPos = 0;
    [HideInInspector]
    public float spawnRightPos = 0;

    void Start()
    {
        if (useSpawnPlacement)
        {
            int spawnCount = Random.Range(spawnCountMin, spawnCountMax);

            for (int i = 0; i < spawnCount; i++)
            {
                SpawnItem();
            }
        }
        else
        {
            speed = Random.Range(speedMin, speedMax);
        }
    }

    void Update()
    {
        if (useSpawnPlacement) return;

        if (Time.time > lastTime + delayTime)
        {
            lastTime = Time.time;
            delayTime = Random.Range(delayMin, delayMax);
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        GameObject obj = PoolObjects.Instance.GetPooledObject(item.tag);

        if (obj != null)
        {
            obj.transform.position = GetSpawnPosition();

            float direction = 0;

            if (gameObject.name == "spawnRight")
            {
                if (obj.GetComponent<Mover>() != null)
                    obj.GetComponent<Mover>().goToLeftSide = true;

                direction = 180;
            }
            else
            {
                if (obj.GetComponent<Mover>() != null)
                    obj.GetComponent<Mover>().goToRightSide = true;
            }

            if (!useSpawnPlacement)
            {
                obj.GetComponent<Mover>().speed = speed;
                obj.transform.rotation = new Quaternion();
                obj.transform.rotation = Quaternion.Euler(0, direction, 0);
            }

            obj.SetActive(true);
        }
    }

    Vector3 GetSpawnPosition()
    {
        if (useSpawnPlacement)
            return new Vector3(Random.Range(spawnLeftPos, spawnRightPos), startPos.position.y, startPos.position.z);
        else
            return startPos.position;
    }
}
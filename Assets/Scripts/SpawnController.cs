using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    bool goLeft = false;
    bool goRight = false;

    public List<GameObject> spawnerItems = new List<GameObject>();
    public List<Spawner> spawnersLeft = new List<Spawner>();
    public List<Spawner> spawnersRight = new List<Spawner>();

    void Start()
    {
        int randomItem1 = Random.Range(0, spawnerItems.Count);
        int randomItem2 = Random.Range(0, spawnerItems.Count);

        GameObject randomGO1 = spawnerItems[randomItem1];
        GameObject randomGO2 = spawnerItems[randomItem2];

        int direction = Random.Range(0, 2);

        if (direction > 0)
        {
            goLeft = false;
            goRight = true;
        }
        else
        {
            goLeft = true;
            goRight = false;
        }

        if (gameObject.tag == "grassOne")
        {
            goLeft = true;
            goRight = true;
        }

        for (int i = 0; i < spawnersLeft.Count; i++)
        {
            if (i % 2 != 0)
                spawnersLeft[i].item = randomGO1;
            else
                spawnersLeft[i].item = randomGO2;

            spawnersLeft[i].gameObject.SetActive(goRight);
            spawnersLeft[i].spawnLeftPos = spawnersLeft[i].transform.position.x;
        }

        for (int i = 0; i < spawnersRight.Count; i++)
        {
            if (i % 2 != 0)
                spawnersRight[i].item = randomGO1;
            else
                spawnersRight[i].item = randomGO2;

            spawnersRight[i].gameObject.SetActive(goLeft);
            spawnersRight[i].spawnLeftPos = spawnersRight[i].transform.position.x;
        }
    }
}
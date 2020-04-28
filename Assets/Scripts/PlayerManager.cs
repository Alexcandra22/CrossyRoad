using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> characters = new List<GameObject>();
    private GameObject player;

    private static PlayerManager instance;
    public static PlayerManager Instance { get { return instance; } }

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
        player = Instantiate(characters[Random.Range(0, characters.Count)]);
        player.transform.position = gameObject.transform.position;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}

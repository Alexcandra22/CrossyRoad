using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> platform = new List<GameObject>();
    public List<float> height = new List<float>();

    private int randomRange = 0;
    private float lastPos = 0;
    private float lastScale = 0;

    private static LevelGenerator instance;
    public static LevelGenerator Instance { get { return instance; } }

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

    public void LevelRandom()
    {
        for (int i = 0; i < platform.Count; i++)
        {
            CreateLevel(platform[i], height[i], i);
        }
    }

    public void CreateLevel(GameObject platform, float height, int value)
    {
        GameObject go = PoolObjectPlatform.Instance.GetPooledObject();

        if (go != null)
        {
            float offset = lastPos + (lastScale * 0.5f);
            offset += (go.transform.localScale.z) * 0.5f;

            Vector3 position = new Vector3(0, height, offset);

            go.transform.position = position;

            lastPos = go.transform.position.z;
            lastScale = go.transform.localScale.z;

            go.transform.parent = transform;
            go.SetActive(true);
        }
    }
}

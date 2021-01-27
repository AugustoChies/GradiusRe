using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSpawner : MonoBehaviour
{
    public GlobalStats stats;
    public GameObject backgroundprefab;
    public Transform scrollerParent;
    public List<GameObject> currentImages;
    public Vector3 spawnPosition;
    // Start is called before the first frame update
    void Awake()
    {
        Reset();       
    }

    // Update is called once per frame
    void Update()
    {
        if (currentImages[1].transform.position.x <= this.transform.position.x) //stars passed midway point
        {
            SpawnBackground(spawnPosition);
            DeleteFifo();
        }
    }

    public void SpawnBackground(Vector3 spawn)
    {
        GameObject token = Instantiate(backgroundprefab, this.transform.position + spawn, Quaternion.identity, scrollerParent);
        currentImages.Add(token);
    }

    public void DeleteFifo()
    {
        GameObject token = currentImages[0];
        currentImages.RemoveAt(0);
        Destroy(token);
    }

    public void Reset()
    {
        for (int i = 0; i < currentImages.Count; i++)
        {
            Destroy(currentImages[i]);
        }
        currentImages = new List<GameObject>();
        SpawnBackground(new Vector3(0.0f, 0.6f, 10.0f));
        SpawnBackground(spawnPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMaster : MonoBehaviour
{
    public List<GameObject> fans;
    public GameObject fanPrefab;
    public int spawnAmount;
    protected int currentSpawnAmount;
    public int killedByPlayer;
    public float spawnTime;

    

    private void Start()
    {
        fans = new List<GameObject>();
        currentSpawnAmount = spawnAmount;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if(fans.Count == 0 && currentSpawnAmount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator SpawnRoutine()
    {        
        GameObject token = Instantiate(fanPrefab, this.transform.position, Quaternion.identity);
        token.GetComponent<facnenemy>().creator = this;
        fans.Add(token);
        currentSpawnAmount--;
        yield return new WaitForSeconds(spawnTime);
        if (currentSpawnAmount > 0)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

}

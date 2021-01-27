using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanMaster : MonoBehaviour
{
    public HazardList hazards;
    public List<GameObject> fans;
    public GameObject fanPrefab;
    public int spawnAmount;
    protected int currentSpawnAmount;
    public int killedByPlayer;
    public float spawnTime;
    public float firstPlayerY;

    private void Awake()
    {
        hazards.misc.Add(this.gameObject);             
    }

    private void Start()
    {
        firstPlayerY = -1000;
        fans = new List<GameObject>();
        currentSpawnAmount = spawnAmount;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        if(fans.Count == 0 && currentSpawnAmount == 0)
        {
            hazards.misc.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(float currentPY)
    {
        if(firstPlayerY == -1000)
        {
            firstPlayerY = currentPY;
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

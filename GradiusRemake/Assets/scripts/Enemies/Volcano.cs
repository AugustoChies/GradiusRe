using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    public GameObject rockPrefab;
    public Vector2 baseVector;
    public List<float> magnitudes;
    public List<float> angles;
    public float rockSpawnDelay;

    protected GameObject token;
    void Start()
    {
        StartCoroutine(SpawnRock());
    }

    public float GetRandomMag()
    {
        int rand = Random.Range(0, magnitudes.Count);
        return magnitudes[rand];
    }

    public Vector2 GetRandDirection()
    {
        int rand = Random.Range(0, angles.Count);
        Vector2 rotated = Quaternion.AngleAxis(angles[rand], Vector3.forward) * baseVector;
        return rotated;
    }

    IEnumerator SpawnRock()
    {
        yield return new WaitForSeconds(rockSpawnDelay);
        token = Instantiate(rockPrefab, this.transform.position, Quaternion.identity);
        token.GetComponent<Rock>().force = GetRandDirection() * GetRandomMag();
        StartCoroutine(SpawnRock());
    }
}

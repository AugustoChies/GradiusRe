using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GlobalStats stats;
    public GameObject spawnable;
    public Transform spawnPosition;
    //private Rigidbody2D rb;
    public bool backSpawn; //if true, enemy spawns from the back
    public SpriteRenderer visualRepresentation;// for stage building purposes
    // Start is called before the first frame update
    void Start()
    {
        visualRepresentation.enabled = false;
    }   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)//camborder
        {
            if (backSpawn && other.transform.position.x > this.transform.position.x)
            {
                Instantiate(spawnable, spawnPosition.position, Quaternion.identity);
            }
            else if (!backSpawn && other.transform.position.x < this.transform.position.x)
            {
                Instantiate(spawnable, spawnPosition.position, Quaternion.identity);
            }
        }
    }
}

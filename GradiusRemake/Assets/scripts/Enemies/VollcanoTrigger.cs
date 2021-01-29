using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VollcanoTrigger : MonoBehaviour
{
    public AudioMaster audio;
    public AudioClip bossMusic;
    public GlobalStats stats;
    public GameObject volcanoPrefab;
    public List<GameObject> spawned;
    public Transform[] spawnObjects;
    public float activeTime;

 

    private void OnTriggerEnter2D(Collider2D other)
    {              
        if (other.gameObject.layer == 8)//camborder
        {
            spawned = new List<GameObject>();
            audio.PlayNewMusicCommand(bossMusic);
            if (this.transform.position.x > Camera.main.transform.position.x)
            {
                for (int i = 0; i < spawnObjects.Length; i++)
                {
                    spawned.Add(Instantiate(volcanoPrefab,spawnObjects[i].position,Quaternion.identity));
                    spawned[i].GetComponent<Volcano>().creator = this;
                }
                StartCoroutine(Eruption());
            }
        }
    }

    IEnumerator Eruption()
    {
        float OSpeed = stats.scrollSpeed;
        stats.scrollSpeed = 0;
        yield return new WaitForSeconds(activeTime);
        stats.scrollSpeed = OSpeed;
        for (int i = 0; i < spawned.Count; i++)
        {
            if(spawned[i] != null)
            {
                spawned[i].GetComponent<Volcano>().SelfDelete();
            }
        }        
    }
}

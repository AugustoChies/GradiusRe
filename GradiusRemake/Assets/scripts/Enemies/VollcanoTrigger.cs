using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VollcanoTrigger : MonoBehaviour
{
    public GlobalStats stats;
    public GameObject[] volcanoObjects;
    public float activeTime;


    private void OnTriggerEnter2D(Collider2D other)
    {      
        
        if (other.gameObject.layer == 8)//camborder
        {
            if (this.transform.position.x > Camera.main.transform.position.x)
            {
                for (int i = 0; i < volcanoObjects.Length; i++)
                {
                    volcanoObjects[i].SetActive(true);                    
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
        for (int i = 0; i < volcanoObjects.Length; i++)
        {
            volcanoObjects[i].SetActive(false);
        }
    }
}

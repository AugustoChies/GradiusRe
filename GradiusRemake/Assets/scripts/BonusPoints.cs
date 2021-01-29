using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    public GlobalStats stats;
    protected bool got;
    void Start()
    {
        got = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !got)
        {
            got = true;
            this.GetComponent<SpriteRenderer>().enabled = true;
            stats.UpdateScore(5000);
            GetComponent<AudioSource>().Play();
            StartCoroutine(SelfDestruct());
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}

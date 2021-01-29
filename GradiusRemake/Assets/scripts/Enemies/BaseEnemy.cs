using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Enemy class, future parent of all enemies, not fully integrated yet
public class BaseEnemy : MonoBehaviour
{
    public HazardList hazards;
    public AudioMaster MAudio;
    public AudioClip deathNoises,damageNoise;
    public int life, scoreValue;
    public float baseSpeed;
    public GlobalStats stats;
    public GameObject PUPrefab;
    public bool dropsPowerUp;
    protected Rigidbody2D rb;
    public Animator spriteAnim;
    public bool isItDed;

    private void Awake()
    {        
        hazards.enemies.Add(this);
    }

    public void Die()
    {
        MAudio.playSoundCommand(deathNoises);
        this.GetComponent<Collider2D>().enabled = false;
        spriteAnim.SetBool("IsDed", true);
        isItDed = true;
        hazards.enemies.Remove(this);
        stats.UpdateScore(scoreValue);
        StartCoroutine(DedNow());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 8)//camborder
        {
            hazards.enemies.Remove(this);
            Destroy(this.gameObject);
        }
        if (col.CompareTag("Bullet"))
        {
            life--;
            MAudio.playSoundCommand(damageNoise);
            if (life <= 0)
                Die();
        }
        else if (col.CompareTag("Player"))
        {
            Die();
        }
    }

   


    IEnumerator DedNow()
    {
        yield return new WaitForSeconds(0.5f);
        if (dropsPowerUp)
        {
            Instantiate(PUPrefab, this.transform.position, Quaternion.identity);
        }        
        Destroy(this.gameObject);
    }
}

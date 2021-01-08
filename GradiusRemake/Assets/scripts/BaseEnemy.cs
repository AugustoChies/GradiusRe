using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Enemy class, future parent of all enemies, not fully integrated yet
public class BaseEnemy : MonoBehaviour
{
    public GlobalStats stats;
    public GameObject PUPrefab;
    public bool dropsPowerUp;
    protected Rigidbody2D rb;
    public Animator spriteAnim;
    public bool isItDed;



    public void Die()
    {
        spriteAnim.SetBool("IsDed", true);
        isItDed = true;        
        //colocar aqui algo q aumente a pontuação.............................................................................................................
        StartCoroutine(DedNow());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bullet"))
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
        this.gameObject.SetActive(false); //desliga o objeto
    }
}

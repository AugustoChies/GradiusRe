using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BaseEnemy
{
    public Vector2 force;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Yeet();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 10)//camborder
        {
            Destroy(this.gameObject);
        }
        if (col.CompareTag("Bullet"))
        {
            life--;
            if (life <= 0)
                Die();
        }
        else if (col.CompareTag("Player"))
        {
            Die();
        }
    }

    public void Die()
    {
        this.GetComponent<Collider2D>().enabled = false;
        spriteAnim.SetBool("IsDed", true);
        isItDed = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        hazards.enemies.Remove(this);
        stats.UpdateScore(scoreValue);
        StartCoroutine(DedNow());
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

    public void Yeet()
    {
        rb.AddForce(force,ForceMode2D.Impulse);
    }
}

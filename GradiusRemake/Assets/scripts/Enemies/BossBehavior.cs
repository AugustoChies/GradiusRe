using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : BaseEnemy
{
    public AudioMaster audio;
    public AudioClip music;
    public float originalScroll;
    public Vector2 heighlimit;
    public float forwardSpeed;
    public float flyForwardTime;
    public float shotDelay;
    protected bool started;
    protected Vector2 movement;
    public GameObject shot;
    protected int maxHP;
    public SpriteRenderer centerSpot;

    private void Start()
    {
        originalScroll = stats.scrollSpeed;
        stats.scrollSpeed = 0;
        rb = this.GetComponent<Rigidbody2D>();
        movement = Vector2.left;
        maxHP = life;
        scoreValue = 10500;
        StartCoroutine(StartFight());
    }

    private void FixedUpdate()
    {
        if (isItDed) return;

        if(life/(float)maxHP < 0.66)
        {
            centerSpot.enabled = false;
            stats.UpdateScore(1000);
        }
        if (life / (float)maxHP < 0.33)
        {
            spriteAnim.SetBool("Half", true);
            stats.UpdateScore(1000);
        }


        Vector2 newPos = rb.position + movement * baseSpeed * Time.deltaTime;
        if(newPos.y > heighlimit.y)
        {
            newPos.y = heighlimit.y;
        }
        else if (newPos.y < heighlimit.x)
        {
            newPos.y = heighlimit.x;
        }
        rb.MovePosition(newPos);
    }

    IEnumerator StartFight()
    {
        yield return new WaitForSeconds(flyForwardTime);
        movement.x = 0;
        movement.y = stats.playerPosition.y > rb.position.y ? 1 : -1;        
        StartCoroutine(ShootTimer());
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(shotDelay);
        if (!isItDed)
        {
            Instantiate(shot, rb.position, Quaternion.identity);
            movement.y = stats.playerPosition.y > rb.position.y ? 1 : -1;
            StartCoroutine(ShootTimer());
        }
    }

    public void Die()
    {
        MAudio.playSoundCommand(deathNoises);
        this.GetComponent<Collider2D>().enabled = false;
        spriteAnim.SetBool("IsDed", true);
        audio.PlayNewMusicCommand(music);
        isItDed = true;
        hazards.enemies.Remove(this);
        stats.scrollSpeed = originalScroll;
        stats.UpdateScore(scoreValue);
        StartCoroutine(DedNow());
    }

    IEnumerator DedNow()
    {
        yield return new WaitForSeconds(0.5f);        
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.CompareTag("Bullet"))
        {
            life--;
            MAudio.playSoundCommand(damageNoise);
            if (life <= 0)
                Die();
        }       
    }
}

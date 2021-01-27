using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facnenemy : BaseEnemy
{
    public FanMaster creator;
    public Vector2 fanDirection = Vector2.zero;
    public Vector2 scrollDirection;
    public bool goingUp;
    private Rigidbody2D fan;

    // Start is called before the first frame update
    void Start()
    {
        isItDed = false; 
        fan = this.GetComponent<Rigidbody2D>();
        goingUp = false;
        fanDirection.x = -4;
    }    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)//camborder
        {
            creator.fans.Remove(this.gameObject);
            hazards.enemies.Remove(this);
            Destroy(this.gameObject);
        }
        else if (col.CompareTag("Bullet"))
        {
            life--;
            if (life <= 0)
            {
                creator.killedByPlayer++;                
                creator.fans.Remove(this.gameObject);
                if(creator.killedByPlayer == creator.spawnAmount)
                {
                    dropsPowerUp = true;
                }
                Die();
            }
        }
        else if (col.CompareTag("Player"))
        {
            creator.killedByPlayer++;
            creator.fans.Remove(this.gameObject);
            if (creator.killedByPlayer == creator.spawnAmount)
            {
                dropsPowerUp = true;
            }            
            Die();
        }
        else if (col.CompareTag("AITrigger"))
        {
            fanDirection.x = 2;
            creator.SetDirection(stats.playerPosition.y);
            if (this.fan.position.y < creator.firstPlayerY)
                fanDirection.y = 2;
            else
                fanDirection.y = -2;
            goingUp = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(goingUp)
            if(((this.fan.position.y - 0.3f) < creator.firstPlayerY) && ((this.fan.position.y + 0.3f) > creator.firstPlayerY))
            {
                goingUp = false;
                fanDirection.y = 0;
                fanDirection.x = 4;
            }
        if(!isItDed)
        {
            fan.MovePosition(this.fan.position + fanDirection.normalized * baseSpeed * Time.deltaTime);
        }
        else//explosion moves along scrolling scenery
        {
            transform.position = this.fan.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
        }
    }
}

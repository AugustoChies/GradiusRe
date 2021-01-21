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
    }    
   
    void Awake()
    {
        fanDirection.x = -4;
        StartCoroutine(IsGoingLeft());
        
    }

/*
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)//camborder
        {
            creator.fans.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
        if (col.CompareTag("Bullet") || CompareTag("Player"))
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
    }
*/
    IEnumerator IsGoingLeft()
    {
        yield return new WaitForSeconds(1.5f);
        fanDirection.x = 2;
        if(this.fan.position.y < stats.playerPosition.y)
            fanDirection.y = 2;
        else
            fanDirection.y = -2;
        goingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(goingUp)
            if(((this.fan.position.y - 0.3f) < stats.playerPosition.y) && ((this.fan.position.y + 0.3f) > stats.playerPosition.y))
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

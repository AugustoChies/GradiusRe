using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facnenemy : BaseEnemy
{
    public FanMaster creator;
    public Vector2 fanDirection = Vector2.zero;
    public Vector2 scrollDirection;
    private bool isStarting;
    private Rigidbody2D fan;

    // Start is called before the first frame update
    void Start()
    {
        isItDed = false; 
        fan = this.GetComponent<Rigidbody2D>();
        isStarting = true;
        fanDirection.x -= 10;
    }    
   

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



    // Update is called once per frame
    void Update()
    {
        if(!isItDed)
        {
            if(isStarting)
            {
                //this.fan.position = {30,15};
                isStarting = false;
            }
            fan.MovePosition(this.fan.position + fanDirection.normalized * baseSpeed * Time.deltaTime);
        }
        else//explosion moves along scrolling scenery
        {
            transform.position = this.fan.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
        }
    }
}

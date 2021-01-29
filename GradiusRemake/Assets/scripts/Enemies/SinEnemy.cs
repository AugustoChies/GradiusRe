using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinEnemy : BaseEnemy
{
    public Vector2 generalDirection = Vector2.zero;
    public Vector2 nullExempleDirection = Vector2.zero;
    public bool goingUp;
    public bool Isded = false;
    public Vector2 scrollDirection = new Vector2(-1,0);
    
    
    

    
    // Start is called before the first frame update
    void Start()
    {        
        rb = this.GetComponent<Rigidbody2D>();
        generalDirection.x -= 5;
        goingUp = true;
        generalDirection.y = nullExempleDirection.y + 3f;
        scoreValue = 100;
    }
   

    // Update is called once per frame
    void Update()
    {
        if(!isItDed)
        {
        if (generalDirection.y >= 6)
            goingUp = false;
        if (generalDirection.y <= -6)
            goingUp = true;

        
            
        if(goingUp)
            generalDirection.y += (20f * Time.deltaTime);//ver aqui valor de velocidade quanto sobe ou desce
        else
            generalDirection.y -= (20f * Time.deltaTime);//ver aqui valor 
        
                
         
        transform.position = this.rb.position + generalDirection * Time.deltaTime; 
        }
        else
            transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
    }
}
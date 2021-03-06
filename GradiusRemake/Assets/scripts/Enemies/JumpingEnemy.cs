﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : BaseEnemy
{
    public Vector2 generalDirection = Vector2.zero;
    public Vector2 nullExempleDirection = Vector2.zero;
    public bool goingLeft;
    public bool canJump;
    public bool jumpStart;
    public int jumps;
    public Vector2 scrollDirection = new Vector2(-1,0);
    public float jumpSpeed;
    public float horizontalSpeed;
    
    
    

    
    // Start is called before the first frame update
    void Start()
    {        
        horizontalSpeed = 4f;
        rb = this.GetComponent<Rigidbody2D>();
        generalDirection.x = -horizontalSpeed;
        jumps = -1;
        goingLeft = false;
        jumpStart = true;
        canJump = false;
        jumpSpeed = 400f;
        scoreValue = 100;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!isItDed)
        {
        if(this.rb.position.y < -3 && jumpStart)//ajustar aqui o valor do chao
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(JumpingNow());
            //jumpSpeed = 600f;
        }

        if(this.rb.position.y < -3 && canJump)//ajustar aqui o valor do chao
        {
            rb.velocity = Vector2.zero;
            StartCoroutine(JumpingNow());
        }   

        if(!goingLeft && (jumps == 2))
        {
            jumps = 0;
            goingLeft = true;
            generalDirection.x = horizontalSpeed;
        }else if(goingLeft && (jumps == 1))
        {
            jumps = 0;
            goingLeft = false;
            generalDirection.x = -horizontalSpeed;
        }
        transform.position = this.rb.position + generalDirection * Time.deltaTime; 
        }else
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
        }


    }


    IEnumerator JumpingNow()
    {
        
        canJump = false;
        jumpStart = false;
        rb.AddForce(new Vector2(0,jumpSpeed));
        jumps++;
        yield return new WaitForSeconds(0.2f);
        canJump = true;
    }
}

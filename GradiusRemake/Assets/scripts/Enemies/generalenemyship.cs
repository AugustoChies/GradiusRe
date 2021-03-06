﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalenemyship : BaseEnemy
{
    public Vector2 generalDirection = Vector2.zero;
    public Vector2 nullExempleDirection = Vector2.zero;
    public float generalSpeed;
    private bool isStarting;
    public bool goingUp;
    public bool goingDown;
    public Vector2 scrollDirection = new Vector2(-1,0);
    
    
    

    
    // Start is called before the first frame update
    void Start()
    {        
        rb = this.GetComponent<Rigidbody2D>();
        isStarting = true;
        generalDirection.x -= 3;
        generalSpeed = 1;
        scoreValue = 100;
    }   

   

    // Update is called once per frame
    void Update()
    {
        generalDirection.y = nullExempleDirection.y;
        generalSpeed = 1;

        if (!isItDed)
        {
            if (isStarting) //aqui vai ser pra quando ele entrar na tela, ainda tem que ver
            {
                isStarting = false;
            }
            if ((stats.playerPosition.y - 0.1) > this.rb.position.y)
            {
                generalDirection.y += 1;//ver aqui valor de velocidade quanto sobe ou desce
                goingUp = true;
                spriteAnim.SetBool("Up", goingUp);
            }
            else
            if ((stats.playerPosition.y + 0.1) < this.rb.position.y)
            {
                generalDirection.y -= 1;//ver aqui valor
                goingDown = true;
                spriteAnim.SetBool("Down", goingDown);
            }
            else
            {
                generalSpeed += 1;
                goingDown = false;
                goingUp = false;
                spriteAnim.SetBool("Down", goingDown);
                spriteAnim.SetBool("Up", goingUp);
            }

            transform.position = this.rb.position + generalDirection * generalSpeed * Time.deltaTime;
        }
        else
        {
            transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
        }
    }
}


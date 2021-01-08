using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalenemyship : BaseEnemy
{
    public Vector2 generalDirection = Vector2.zero;
    public Vector2 nullExempleDirection = Vector2.zero;
    public Vector2 deathPos = Vector2.zero;
    private float generalSpeed;
    private bool isStarting;
    public bool goingUp;
    public bool goingDown;
    public bool Isded = false;
    
    
    

    
    // Start is called before the first frame update
    void Start()
    {        
        rb = this.GetComponent<Rigidbody2D>();
        isStarting = true;
        generalDirection.x -= 10;
        generalSpeed = 1;
    }   

   

    // Update is called once per frame
    void Update()
    {
        generalDirection.y = nullExempleDirection.y;


        if(!Isded)
        {
            if(isStarting) //aqui vai ser pra quando ele entrar na tela, ainda tem que ver
            {
                isStarting = false;
            }
            if((stats.playerPosition.y - 0.1) > this.rb.position.y)
            {
                generalDirection.y += 5;//ver aqui valor de velocidade quanto sobe ou desce
                goingUp = true;
                spriteAnim.SetBool("Up", goingUp);
            }else
            if((stats.playerPosition.y + 0.1) < this.rb.position.y)
            {
                generalDirection.y -= 5;//ver aqui valor
                goingDown = true;
                spriteAnim.SetBool("Down", goingDown);
            }else
            {
                goingDown = false;
                goingUp = false;
                spriteAnim.SetBool("Down", goingDown);
                spriteAnim.SetBool("Up", goingUp);
            }        
            
            rb.MovePosition(this.rb.position + generalDirection * generalSpeed * Time.deltaTime);
        }
    }
}


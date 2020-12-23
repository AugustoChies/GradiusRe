using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalenemyship : MonoBehaviour
{
    public GlobalStats stats;
    public Vector2 generalDirection = Vector2.zero;
    public Vector2 nullExempleDirection = Vector2.zero;
    private float generalSpeed;
    private bool isStarting;
    public bool goingUp;
    public bool goingDown;
    
    private Rigidbody2D general;
    public Animator spriteAnim;
    

    
    // Start is called before the first frame update
    void Start()
    {
        
        general = this.GetComponent<Rigidbody2D>();
        isStarting = true;
        generalDirection.x += 3;
        generalSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        generalDirection.y = nullExempleDirection.y;
        if(isStarting) //aqui vai ser pra quando ele entrar na tela, ainda tem que ver
        {
            isStarting = false;
        }
        if((stats.playerPosition.y - 0.1) > this.general.position.y)
        {
            generalDirection.y += 5;//ver aqui valor de velocidade quanto sobe ou desce
            goingUp = true;
            spriteAnim.SetBool("Up", goingUp);
        }else
        if((stats.playerPosition.y + 0.1) < this.general.position.y)
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
            
        
    
        general.MovePosition(this.general.position + generalDirection * generalSpeed * Time.deltaTime);
    }
}



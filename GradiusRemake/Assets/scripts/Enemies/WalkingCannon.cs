using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCannon : BaseEnemy
{

    public Vector2 generalDirection = Vector2.zero;
    public bool farEnough; 
    public bool stopNow;
    public bool walkNow;
    public Vector2 nullExempleDirection = Vector2.zero;
    public GameObject CannonTiro;
    public float xDistanceToShip;
    public float yDistanceToShip;
    public Vector2 scrollDirection = new Vector2(-1,0);

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        generalDirection.x += 10;
        farEnough = false;
        walkNow = true;
        stopNow = false;
        
    }


    void Awake()
    {
        StartCoroutine(WalkingNow());
    }

    IEnumerator WalkingNow()
    {
        walkNow = true;
        spriteAnim.SetBool("IsWalking", true);
        yield return new WaitForSeconds(1.5f);
        if (farEnough)
        {
            walkNow = false;
            spriteAnim.SetBool("IsWalking", false);
            StartCoroutine(AimingNow());
        }
            
        else
            StartCoroutine(WalkingNow()); 
    }

    IEnumerator AimingNow()
    {
        stopNow = true;
        spriteAnim.SetBool("StopedWalking", true);
        yield return new WaitForSeconds(1.5f);
        ShootTheShip();
        yield return new WaitForSeconds(0.5f);
        if (farEnough)
            StartCoroutine(AimingNow());
        else
        {
            stopNow = false;
            spriteAnim.SetBool("StopedWalking", false);
            StartCoroutine(WalkingNow());
        }
    }

    private void ShootTheShip() //colocar aqui um método que dispare um tiro contra a nave
    {       
        Vector2 playPos = new Vector2(xDistanceToShip, yDistanceToShip);

        GameObject token;
        
        token = Instantiate(CannonTiro, this.rb.position, Quaternion.identity);
        token.GetComponent<CannonShot>().direction = playPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isItDed)
        {
        generalDirection.x = nullExempleDirection.x;
        xDistanceToShip = stats.playerPosition.x - this.rb.position.x;
        yDistanceToShip = stats.playerPosition.y - this.rb.position.y;



        if((stats.playerPosition.x + 3) > this.rb.position.x)
            farEnough = false;
        else
            farEnough = true;
        
        

        if (walkNow)
            generalDirection.x = 6;
        else if (stopNow)
            generalDirection.x = -stats.scrollSpeed;

        transform.position = this.rb.position + generalDirection * Time.deltaTime; 
        }
        else
            transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
        
    }
}

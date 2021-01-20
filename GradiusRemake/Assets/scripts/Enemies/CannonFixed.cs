using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFixed : BaseEnemy
{

    public Vector2 generalDirection = Vector2.zero;
    
    public float xDistanceToShip;
    public float yDistanceToShip;
    public float xDistanceToShipShoot;
    public float yDistanceToShipShoot;
    public float diagonal;
    public GameObject CannonTiro;
    public Vector2 scrollDirection = new Vector2(-1,0);
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
        
    }

    void Awake()
    {
        
        StartCoroutine(Starting());
        StartCoroutine(TimeToShoot());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        xDistanceToShip = stats.playerPosition.x - this.rb.position.x;
        yDistanceToShip = stats.playerPosition.y - this.rb.position.y;
        xDistanceToShipShoot = xDistanceToShip;
        yDistanceToShipShoot = yDistanceToShip;
        if(yDistanceToShip < 0)
            yDistanceToShip = yDistanceToShip * -1;
        if(xDistanceToShip < 0)
            xDistanceToShip = xDistanceToShip * -1;

        diagonal = xDistanceToShip/yDistanceToShip;
        
        if(diagonal < 1)
        {
            spriteAnim.SetBool("IsLow", false);
            spriteAnim.SetBool("IsMid", false);
            spriteAnim.SetBool("IsHi", true);
        } else if(diagonal < 3)
        {
            spriteAnim.SetBool("IsLow", false);
            spriteAnim.SetBool("IsMid", true);
            spriteAnim.SetBool("IsHi", false);
        } else
        {
            spriteAnim.SetBool("IsLow", true);
            spriteAnim.SetBool("IsMid", false);
            spriteAnim.SetBool("IsHi", false);
        }

        transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
    }


    IEnumerator Starting()
    {
        yield return new WaitForSeconds(1.5f); //mudar aqui o tempo
        spriteAnim.SetBool("IsMid", true);
    }


    IEnumerator TimeToShoot()
    {
        yield return new WaitForSeconds(3f); //mudar aqui o tempo
        ShootTheShip();
        StartCoroutine(TimeToShoot());
    }

    private void ShootTheShip() //colocar aqui um método que dispare um tiro contra a nave
    {       
        Vector2 playPos = new Vector2(xDistanceToShipShoot, yDistanceToShipShoot);

        GameObject token;
        
        token = Instantiate(CannonTiro, this.rb.position, Quaternion.identity);
        token.GetComponent<CannonShot>().direction = playPos;
    }
}

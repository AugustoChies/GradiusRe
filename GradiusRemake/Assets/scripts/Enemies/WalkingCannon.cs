using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCannon : BaseEnemy
{
    public Transform[] topRays, bottomRays;
    public float rayDistance, originalY, climbSpeed;
    public int stopCounter;
    public SpriteRenderer myRenderer;
    public Vector2 generalDirection = Vector2.zero;
    public bool farEnough; 
    public bool stopNow;
    public bool walkNow;
    public Vector2 nullExempleDirection = Vector2.zero;
    public GameObject CannonTiro;
    public Vector2 scrollDirection = new Vector2(-1,0);

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        farEnough = false;
        farEnough = false;
        walkNow = true;
        stopNow = false;
        originalY = rb.position.y;
        scoreValue = 100;
        StartCoroutine(WalkingNow());
    }

    public bool CheckHorizontalTerrain()
    {
        Vector2 rayOrigin = Vector2.zero;
        Vector2 rayDirection = Vector2.zero;

        if (myRenderer.flipY)
        {
            if(myRenderer.flipX)
            {
                rayOrigin = topRays[1].position;
                rayDirection = Vector2.left;
            }
            else
            {
                rayOrigin = topRays[0].position;
                rayDirection = Vector2.right;
            }
        }
        else
        {
            if (myRenderer.flipX)
            {
                rayOrigin = bottomRays[1].position;
                rayDirection = Vector2.left;
            }
            else
            {
                rayOrigin = bottomRays[0].position;
                rayDirection = Vector2.right;
            }
        }

        RaycastHit2D raycast = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, 1 << LayerMask.NameToLayer("Ground"));
        return (raycast.collider != null && raycast.collider.CompareTag("Climbable"));
    }

    public bool CheckVerticalTerrain()
    {
        Vector2 rayOrigin = Vector2.zero;
        Vector2 rayDirection = Vector2.zero;

        if (myRenderer.flipY)
        {
            rayDirection = Vector2.up;
            if (myRenderer.flipX)
            {
                rayOrigin = topRays[1].position;                
            }
            else
            {
                rayOrigin = topRays[0].position;
            }
        }
        else
        {
            rayDirection = Vector2.down;
            if (myRenderer.flipX)
            {
                rayOrigin = bottomRays[1].position;                
            }
            else
            {
                rayOrigin = bottomRays[0].position;
            }
        }

        RaycastHit2D raycast = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, 1 << LayerMask.NameToLayer("Ground"));
        return (raycast.collider != null && raycast.collider.CompareTag("Climbable"));
    }

    IEnumerator WalkingNow()
    {
        walkNow = true;
        spriteAnim.SetBool("IsWalking", true);
        yield return new WaitForSeconds(1.25f);

        if (stopCounter > 0)
        {
            walkNow = false;
            spriteAnim.SetBool("IsWalking", false);
            StartCoroutine(AimingNow());
        }
        else
        {
            StartCoroutine(WalkingNow());
        }

    }

    IEnumerator AimingNow()
    {
        stopNow = true;
        stopCounter--;
        spriteAnim.SetBool("StopedWalking", true);
        if(stats.playerPosition.x < this.transform.position.x)
        {
            myRenderer.flipX = true;
        }
        yield return new WaitForSeconds(1.5f);
        ShootTheShip();
        yield return new WaitForSeconds(0.5f);
        
       
        stopNow = false;
        myRenderer.flipX = false;
        spriteAnim.SetBool("StopedWalking", false);
        StartCoroutine(WalkingNow());        
    }

    private void ShootTheShip() //colocar aqui um método que dispare um tiro contra a nave
    {       
        Vector2 playPos = new Vector2(stats.playerPosition.x - this.transform.position.x, stats.playerPosition.y - this.transform.position.y);

        GameObject token;
        
        token = Instantiate(CannonTiro, this.rb.position, Quaternion.identity);
        token.GetComponent<CannonShot>().direction = playPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isItDed)
        {
            generalDirection.x = nullExempleDirection.x;
            generalDirection.y = nullExempleDirection.y;

            if (walkNow)
            {               
                     
                generalDirection.x = baseSpeed;

                if (stopCounter <= 0)
                {
                    myRenderer.flipX = true;
                    generalDirection.x *= -1;
                }

                if(CheckHorizontalTerrain())
                {
                    if(myRenderer.flipY)
                    {
                        generalDirection.y = -1 * climbSpeed;
                    }
                    else
                    {
                        generalDirection.y = climbSpeed;
                    }
                }
                else
                {
                    if (!CheckVerticalTerrain())
                    {
                        if (myRenderer.flipY)
                        {
                            if (transform.position.y < originalY)
                            {
                                generalDirection.y = climbSpeed;
                            }
                        }
                        else
                        {
                            if (transform.position.y > originalY)
                            {
                                generalDirection.y = -1 * climbSpeed;
                            }
                        }
                    }
                }
            }
            else if (stopNow)
            {
                generalDirection.x = -stats.scrollSpeed;
                generalDirection.y = 0;
            }
            
            transform.position = this.rb.position + generalDirection * Time.deltaTime;
                       
        }
        else
        {
            transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
        }
    }
}

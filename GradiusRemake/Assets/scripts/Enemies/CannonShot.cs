using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 direction;
    
    public GlobalStats stats;
    

    protected Rigidbody2D rb;
    
    private int generalSpeed;

    protected void Awake()
    {
        generalSpeed = 3;
        rb = this.GetComponent<Rigidbody2D>();
        
    }


    protected void FixedUpdate()
    {
        rb.MovePosition(this.rb.position + direction.normalized * generalSpeed * Time.deltaTime);
    }

    

    

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10) //camborder and ground
        {
            this.gameObject.SetActive(false);
        } else if(collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }
        
    }

  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 direction;
    
    public GlobalStats stats;
    

    protected Rigidbody2D rb;
    
    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        
    }


    protected void FixedUpdate()
    {
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }

    

    

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10) //camborder and ground
        {
            Destroy(this.gameObject);
        } else if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
        
    }

  

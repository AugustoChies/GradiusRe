using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 direction;
    
    public GlobalStats stats;
    public HazardList hazard;

    protected Rigidbody2D rb;
    
    protected void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        hazard.misc.Add(this.gameObject);
    }


    protected void FixedUpdate()
    {
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }

    

    

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10) //camborder and ground
        {
            hazard.misc.Remove(this.gameObject);
            Destroy(this.gameObject);
        } else if(collision.CompareTag("Player"))
        {
            hazard.misc.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
        
    }

  

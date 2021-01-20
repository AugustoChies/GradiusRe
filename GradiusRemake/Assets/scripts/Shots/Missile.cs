using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Shot
{    
    protected int contacts;
    [SerializeField]
    protected Vector2 groundDirection,airDirection;
    public Animator anim;


    public override void Move()
    {
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) //camborder
        {
            Deactivate();
        }
        else if (collision.gameObject.layer == 10) //ground
        {
            anim.SetBool("Floor", true);
            contacts += 1;
            Debug.Log(1);            
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(contacts > 0)
        {
            if(collision.CompareTag("Ground"))
            {
                direction = groundDirection;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (contacts > 0)
        {
            if (collision.CompareTag("Ground"))
            {                
                contacts -= 1;
                Debug.Log(-1);
            }
            if (contacts == 0)
            {
                anim.SetBool("Floor", false);
                direction = airDirection;
            }
        }
    }
}

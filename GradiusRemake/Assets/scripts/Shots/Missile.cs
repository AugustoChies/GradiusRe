using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Shot
{    
    protected bool contact;
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
            if (rb.position.y > collision.gameObject.transform.position.y)
            {
                anim.SetBool("Floor", true);
                contact = true;
            }
            else
            {
                Deactivate();
            }
        }        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(contact)
        {
            if(collision.CompareTag("Ground"))
            {
                direction = groundDirection;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (contact)
        {
            if (collision.CompareTag("Ground"))
            {
                direction = airDirection;
                contact = false;
                anim.SetBool("Floor", false);
                direction = airDirection;
            }
        }
    }
}

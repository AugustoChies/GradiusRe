using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Shot
{    
    protected int contacts;
    [SerializeField]
    protected Vector2 groundDirection,airDirection;
    public Animator anim;
    public Transform rayOrigin;
    public float rayDistance;


    public override void Move()
    {
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
        CheckTerrain();
    }

    public bool CheckTerrain()
    {
        RaycastHit2D raycast = Physics2D.Raycast(rayOrigin.position, Vector2.right, rayDistance, 1 << LayerMask.NameToLayer("Ground"));         
        return raycast.collider != null;
    }

    public void ComplexDeactivate()
    {
        anim.SetBool("Floor", false);
        direction = airDirection;
        Deactivate();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) //camborder
        {
            ComplexDeactivate();
        }
        else if (collision.gameObject.layer == 10) //ground
        {
            if(CheckTerrain())//hitting slope
            {
                ComplexDeactivate();
            }
            anim.SetBool("Floor", true);
            contacts += 1;
            direction = groundDirection;
        }
        else if (collision.CompareTag("Enemy"))
        {
            ComplexDeactivate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (contacts > 0)
        {
            if (collision.gameObject.layer == 10) //ground
            {                
                contacts -= 1;
            }
            if (contacts == 0)
            {
                anim.SetBool("Floor", false);
                direction = airDirection;
            }
        }
    }
}

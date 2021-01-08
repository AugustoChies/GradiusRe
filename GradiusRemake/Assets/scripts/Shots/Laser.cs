using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Shot
{
    public override void Move()
    {
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10) //camborder and ground
        {
            Deactivate();
        }        
    }
}

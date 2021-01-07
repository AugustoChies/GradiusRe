using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShot : Shot
{
    public override void Move()
    {
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }    
}

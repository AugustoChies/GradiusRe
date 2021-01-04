using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightShot : Shot
{
    public override void Move()
    {
        rb.position = this.rb.position + Vector2.right * stats.scrollSpeed * Time.deltaTime;
        rb.MovePosition(this.rb.position + direction.normalized * moveSpeed * Time.deltaTime);
    }
}

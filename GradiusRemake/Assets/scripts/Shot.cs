using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shot : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 direction;
    public bool isActive;
    public GlobalStats stats;

    protected Rigidbody2D rb;

    protected void Awake()
    {
        isActive = false;
    }


    protected void FixedUpdate()
    {
        if(isActive)
            Move();
    }

    public abstract void Move();
    
}

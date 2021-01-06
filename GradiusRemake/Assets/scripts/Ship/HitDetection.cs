﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public delegate void GotHit();
    public GotHit hitByEnemy;
    public Rigidbody2D parentPosition;
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(parentPosition.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            hitByEnemy();
        }
    }
}

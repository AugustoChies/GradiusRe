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


    public List<Shot> activeList,stashList;
    public Vector3 stashPos;

    protected void Awake()
    {
        isActive = false;
        rb = this.GetComponent<Rigidbody2D>();
    }


    protected void FixedUpdate()
    {
        if(isActive)
            Move();
    }

    public abstract void Move();

    public void Activate(Vector3 newPosition,List<Shot> newActiveList)
    {
        activeList = newActiveList;
        isActive = true;
        rb.position = newPosition;
        activeList.Add(this);
        stashList.Remove(this);
    }

    public void Deactivate()
    {
        isActive = false;
        rb.position = stashPos;
        activeList.Remove(this);
        stashList.Add(this);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10) //camborder and ground
        {
            Deactivate();
        }
        else if(collision.CompareTag("Enemy"))
        {
            Deactivate();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facnenemy : BaseEnemy
{
    public Vector2 fanDirection = Vector2.zero;
    private float fanSpeed;
    private bool isStarting;
    private Rigidbody2D fan;

    // Start is called before the first frame update
    void Start()
    {
        isItDed = false; 
        fan = this.GetComponent<Rigidbody2D>();
        isStarting = true;
        fanDirection.x -= 10;
        fanSpeed = 10;
    } 

    

    // Update is called once per frame
    void Update()
    {
        if(!isItDed)
        {
            if(isStarting)
            {
                //this.fan.position = {30,15};
                isStarting = false;
            }
            fan.MovePosition(this.fan.position + fanDirection.normalized * fanSpeed * Time.deltaTime);
        }
    }
}

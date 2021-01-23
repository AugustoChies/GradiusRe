using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonEnemy : BaseEnemy
{

    
    public Vector2 generalDirection = Vector2.zero;
    public GameObject CannonTiro;
    public float xDistanceToShip;
    public float yDistanceToShip;
    public bool hasntShot;
    public bool hasReached;
    public Vector2 scrollDirection = new Vector2(-1,0);


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        hasntShot = true;
        hasReached = false;

      // if(stats.playerPosition.y > this.rb.position.y)
      //     generalDirection.y = 2;
      // else
      //     generalDirection.y =-2;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isItDed)
        {
        xDistanceToShip = stats.playerPosition.x - this.rb.position.x;//ver se n dá problema ou se tem q normalizar
        yDistanceToShip = stats.playerPosition.y - this.rb.position.y;
        

        if((yDistanceToShip > -0.2f) && (yDistanceToShip < 0.2f))
            hasReached = true;
        
        if(hasReached)
        {
            generalDirection.y = 0;
            generalDirection.x = -3;
        }

        if(hasntShot && hasReached)
            if((xDistanceToShip + yDistanceToShip < 3) && (xDistanceToShip + yDistanceToShip > -3)) //se eles podem atirar antes de ficarem alinhados com o player é só tirar o hasreached daqui
        {
            ShootTheShip();
            hasntShot = false;
        }

        transform.position = this.rb.position + generalDirection * Time.deltaTime;

        }
        else
            transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
    }


    private void ShootTheShip() //colocar aqui um método que dispare um tiro contra a nave
    {       
        Vector2 playPos = new Vector2(xDistanceToShip, yDistanceToShip);

        GameObject token;
        
        token = Instantiate(CannonTiro, this.rb.position, Quaternion.identity);
        token.GetComponent<CannonShot>().direction = playPos;
    }
}

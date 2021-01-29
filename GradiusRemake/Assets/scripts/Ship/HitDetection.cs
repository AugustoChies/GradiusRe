using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    public delegate void GotHit(int damage);
    public delegate void Contact();
    public GotHit hitByEnemy;
    public Contact hitGround, gotPowerUp, gotBlu;
    public Rigidbody2D parentPosition;
    protected Rigidbody2D rb;
    public bool iframes;

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
        if (iframes) return;
        
        if(collision.CompareTag("EnemyBullet"))
        {
            hitByEnemy(1);
        }
        else if (collision.CompareTag("Enemy")) // destroy shield and enemy at fast rate when shield is active
        {

            hitByEnemy(collision.GetComponent<BaseEnemy>().life);
        }        
        else if (collision.gameObject.layer == 10)
        {
            hitGround();
        }

        if (collision.CompareTag("Power"))
        {

            gotPowerUp();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("PowerBlu"))
        {

            gotBlu();
            Destroy(collision.gameObject);
        }
    }    

    public void SetIFrames(float time)
    {
        iframes = true;
        StartCoroutine(IFrameTime(time));
    }

    IEnumerator IFrameTime(float time)
    {
        yield return new WaitForSeconds(time);
        iframes = false;
    }
}

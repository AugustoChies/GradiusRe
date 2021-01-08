using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facnenemy : MonoBehaviour
{
    public GlobalStats stats;
    public Vector2 fanDirection = Vector2.zero;
    private float fanSpeed;
    private bool isStarting;
    private Rigidbody2D fan;
    public bool isItDed;
    public Animator spriteAnim;

    // Start is called before the first frame update
    void Start()
    {
        isItDed = false; 
        fan = this.GetComponent<Rigidbody2D>();
        isStarting = true;
        fanDirection.x -= 10;
        fanSpeed = 10;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        
        
        if(col.CompareTag("Bullet"))
            {
                spriteAnim.SetBool("IsDed", true);
                isItDed = true;
                //colocar aqui algo q aumente a pontuação.............................................................................................................
                StartCoroutine(DedNow());
                
                
            }
            
    }

    IEnumerator DedNow()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false); //desliga o objeto
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

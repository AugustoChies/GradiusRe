using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherEnemy : BaseEnemy
{
    public Vector2 generalDirection = Vector2.zero;
    public GameObject Son;
    public int howMany;
    public int waiting;
    public bool canStart = false;
    public Vector2 scrollDirection = new Vector2(-1,0);


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        canStart = true;       
        StartCoroutine(CreateSon());
    }


    IEnumerator CreateSon()
    {
        yield return new WaitForSeconds(1f);
        if(howMany < 4)
        {
            howMany++;
            waiting++;
            CreatingSon();
            StartCoroutine(CreateSon());
        }
        else
        {
            waiting--;
            if(waiting == 0)
                howMany = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(life == 1 && canStart)
            spriteAnim.SetBool("LoLife", true);



        transform.position = this.rb.position + scrollDirection * stats.scrollSpeed * Time.deltaTime;
    }

    private void CreatingSon()
    {
       Instantiate(Son, this.rb.position, Quaternion.identity);
    }

}

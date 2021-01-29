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
    public Vector2 spawnPlace = Vector2.zero;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        life = 4;
        canStart = true;
        scoreValue = 1000;
        StartCoroutine(CreateSon());
    }

    IEnumerator CreateSon()
    {
        for (int i = 0; i < 4; i++)
        {
        yield return new WaitForSeconds(0.5f);
            CreatingSon();
        }
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(CreateSon());
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
        spawnPlace = this.rb.position;
        spawnPlace.y += 1;
        Instantiate(Son, spawnPlace, Quaternion.identity);
    }

}

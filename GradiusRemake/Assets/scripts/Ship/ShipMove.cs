using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipMove : MonoBehaviour
{
    public GlobalStats stats;
    public ControlsObj controls;
    public Vector2 direction { get; private set; }
    public UpgradeController controller;
    [SerializeField]
    private float speed;


    private Rigidbody2D rb;

    public Animator spriteAnim,shieldAnim;
    public SpriteRenderer shieldSprite;
    public HitDetection hd;
    public int shieldMaxHp;
    protected int shieldHP;


    public int pastPositionsSizeLimit;
    public int optionMovementStartDelay;
    protected List<GameObject> options;
    public GameObject optionPrefab;
    protected List<Vector2> pastPositions;
    // Start is called before the first frame update
    void Awake()
    {
        pastPositions = new List<Vector2>();        
        options = new List<GameObject>();
        rb = this.GetComponent<Rigidbody2D>();
        shieldSprite.enabled = false;
        shieldAnim.enabled = false;
        controller = GameObject.Find("Canvas").GetComponent<UpgradeController>();
        if (controller == null)
        {
            throw new Exception("UpgradeController Object not Found");
        }
        controller.optionAction += SpawnOption;
        controller.shieldAction += ActivateShield;
        hd.hitByEnemy += GetHit;
        hd.hitGround += Die;
        pastPositions.Add(rb.position);//add position 0
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 temp_direction = Vector2.zero;
        if (Input.GetKey(controls.up))
        {
            temp_direction.y += 1;
        }
        if (Input.GetKey(controls.down))
        {
            temp_direction.y -= 1;
        }
        if (Input.GetKey(controls.left))
        {
            temp_direction.x -= 1;
        }
        if (Input.GetKey(controls.right))
        {
            temp_direction.x += 1;
        }

        direction = temp_direction;

        //DELETE LATER
        if(Input.GetKeyDown(KeyCode.U))
        {
            controller.NextCurrent();
        }       
    }

    private void FixedUpdate()
    {       
        rb.MovePosition(this.rb.position + direction.normalized * (speed + (speed/2 * controller.speedboost)) * Time.deltaTime);
        if(direction != Vector2.zero) // Only during movement
        {
            pastPositions.Add(rb.position);
            if(pastPositions.Count > pastPositionsSizeLimit)
            {
                pastPositions.RemoveAt(0);                
            }
            for (int i = 0; i < options.Count; i++)
            {
                options[i].GetComponent<OptionBehavoir>().Move();
            }
        }
        spriteAnim.SetInteger("Direction", (int)direction.y);
        stats.playerPosition = rb.position; 
    }

    public void SpawnOption()
    {
        GameObject token;
        if (pastPositions.Count < optionMovementStartDelay * (options.Count + 1))
        {
            token = Instantiate(optionPrefab, pastPositions[0], Quaternion.identity);
        }
        else
        {
            token = Instantiate(optionPrefab, pastPositions[pastPositions.Count - 1 - optionMovementStartDelay * (options.Count + 1)], Quaternion.identity);
        }
        token.GetComponent<OptionBehavoir>().moveDelay = optionMovementStartDelay * (options.Count + 1);
        token.GetComponent<OptionBehavoir>().pastPositions = pastPositions;
        options.Add(token);        
    }

    public void ActivateShield()
    {
        shieldSprite.enabled = true;
        shieldAnim.enabled = true;
        shieldHP = shieldMaxHp;
        shieldAnim.SetBool("Weak", false);
    }

    public void DisableShield()
    {
        shieldSprite.enabled = false;
        shieldAnim.enabled = false;
        controller.DisableShieldIcon();
    }

    public void GetHit()
    {
        if(shieldHP > 0)
        {
            shieldHP--;
            if(shieldHP == 1)
            {
                shieldAnim.SetBool("Weak", true);
            }
            if (shieldHP == 0)
            {
                DisableShield();
            }
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        //die
        Debug.Log("I am die. Thank you forever.");
    }

}

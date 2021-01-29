using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ShipMove : MonoBehaviour
{
    public GlobalStats stats;
    public AudioMaster Maudio;
    public AudioClip gameover,death,grabPowerUp;
    public HazardList hazards;
    public int powerUpScore;
    public ControlsObj controls;
    public Vector2 direction { get; private set; }
    public UpgradeController controller;
    public CheckpointMaster checkpoints;
    public ShipShooting shootingScript;
    [SerializeField]
    private float speed;

    private Vector2 originalPos;
    private Rigidbody2D rb;

    public Animator spriteAnim,shieldAnim;
    public SpriteRenderer shieldSprite;
    public HitDetection hd;
    public int shieldMaxHp;
    public int shieldHP;

    public bool isDed;

    public int pastPositionsSizeLimit;
    public int optionMovementStartDelay;
    protected List<GameObject> options;
    public GameObject optionPrefab;
    protected List<Vector2> pastPositions;
    // Start is called before the first frame update
    void Awake()
    {
        stats.scrollSpeed = 1;
        stats.paused = false;
        hazards.Initialize();
        pastPositions = new List<Vector2>();        
        options = new List<GameObject>();
        rb = this.GetComponent<Rigidbody2D>();
        originalPos = rb.position;
        shieldSprite.enabled = false;
        shieldAnim.enabled = false;
        controller = GameObject.Find("Canvas").GetComponent<UpgradeController>();
        checkpoints = GameObject.Find("CheckPointMaster").GetComponent<CheckpointMaster>();
        shootingScript = this.GetComponent<ShipShooting>();
        if (shootingScript == null)
        {
            throw new Exception("ShipShooting not Found");
        }
        shootingScript.optionsRef = options;
        controller.optionAction += SpawnOption;
        controller.shieldAction += ActivateShield;
        hd.hitByEnemy += GetHit;
        hd.hitGround += Die;
        hd.gotPowerUp += GetPowerUp;
        hd.gotBlu += GetBlu;
        pastPositions.Add(rb.position);//add position 0
        isDed = false;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (stats.paused) return;
        Vector2 temp_direction = Vector2.zero;
        if (Input.GetKey(controls.up))
        {
            if(!isDed)
            temp_direction.y += 1;
        }
        if (Input.GetKey(controls.down))
        {
            if(!isDed)
            temp_direction.y -= 1;
        }
        if (Input.GetKey(controls.left))
        {
            if(!isDed)
            temp_direction.x -= 1;
        }
        if (Input.GetKey(controls.right))
        {
            if(!isDed)
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
        if (stats.paused) return;
        rb.MovePosition(this.rb.position + direction.normalized * (speed + (speed/3 * controller.speedboost)) * Time.deltaTime);
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
        token.GetComponent<OptionBehavoir>().AssignInactiveLists(shootingScript.inactiveRegShots,shootingScript.inactiveUpShots,
            shootingScript.inactiveLaserShots, shootingScript.inactiveMisShots);
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

    public void GetPowerUp()
    {
        this.GetComponent<AudioSource>().clip = grabPowerUp;
        this.GetComponent<AudioSource>().Play();
        controller.NextCurrent();
        stats.UpdateScore(powerUpScore);
    }

    public void GetBlu()
    {
        this.GetComponent<AudioSource>().clip = grabPowerUp;
        this.GetComponent<AudioSource>().Play();
        hazards.KillEnemies();
        stats.UpdateScore(powerUpScore);
    }

    public void GetHit(int damage)
    {
        if(shieldHP > 0)
        {
            shieldHP-= damage;
            if(shieldHP <= 0)
            {
                DisableShield();
                if(shieldHP < 0)
                {
                    Die();
                }                   
            }
            else if(shieldHP == 1)
            {
                shieldAnim.SetBool("Weak", true);
            }            
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDed) return;
        Maudio.StopMusicCommand();
        spriteAnim.SetBool("IsDed", true);
        isDed = true;
        shootingScript.ded = true;
        this.GetComponent<Collider2D>().enabled = false;
        hd.gameObject.GetComponent<Collider2D>().enabled = false;
        shieldSprite.enabled = false;
        shieldAnim.enabled = false;        
        StartCoroutine(DedNow());        
    }

    IEnumerator DedNow()
    {
        this.GetComponent<AudioSource>().clip = death;
        this.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.7f);//wait for animation to finish
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);//wait for sound to finish
        if (stats.playerLifes - 1 >= 0)
        {
            Respawn();
        }
        else
        {
            StartCoroutine(GameOver());
        }
    }

    public void OptionsReset()
    {
        for (int i = 0; i < options.Count; i++)
        {
            Destroy(options[i]);
        }
        options.Clear();
        pastPositions = new List<Vector2>();
        pastPositions.Add(rb.position);//add position 0
    }

    public void Respawn()
    {
        hazards.ClearScreen();
        rb.position = originalPos;
        stats.UpdateLife(-1);
        stats.scrollSpeed = 1;
        spriteAnim.SetBool("IsDed", false);
        isDed = false;
        shootingScript.ded = false;
        checkpoints.MoveStage();
        this.GetComponent<Collider2D>().enabled = true;
        OptionsReset();
        controller.DeathReset();
        hd.SetIFrames(0.5f);
        hd.gameObject.GetComponent<Collider2D>().enabled = true;
        this.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Camera.main.gameObject.GetComponent<BackGroundSpawner>().Reset();
    }

    IEnumerator GameOver()
    {
        Maudio.PlayNewMusicCommand(gameover);
        stats.UpdateLife(-1);
        stats.scrollSpeed = 0;
        yield return new WaitForSeconds(6f);
        ReturnToMenu();
    }

    public void ReturnToMenu()
    {
        if(stats.score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore",stats.score);           
        }
        SceneManager.LoadScene("MainMenu");
    }



}

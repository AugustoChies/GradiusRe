using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipShooting : MonoBehaviour
{
    public List<Shot> inactiveRegShots, activeRegShots, inactiveUpShots, activeUpShots,
       inactiveLaserShots, activeLaserShots, activeMisShots, inactiveMisShots;
    public ControlsObj controls;
    protected float continuousShotDelay;//Holding fire button causes slower continuous shooting
    public float holdFireTime;
    public AudioSource shotSource;
    public AudioClip shotSound, laserSound;
    public GameObject regularshot, upwardShot, laser, missile;
    protected Vector3 stashPos = new Vector3(0, 100, 0);
    public Transform fireOriginPos;
    public UpgradeController controller;
    public bool ded;
    public List<GameObject> optionsRef;
    public GlobalStats stats;
    // Start is called before the first frame update
    void Awake()
    {
        activeRegShots = new List<Shot>();
        inactiveRegShots = new List<Shot>();
        activeUpShots = new List<Shot>();
        inactiveUpShots = new List<Shot>();
        activeLaserShots = new List<Shot>();
        inactiveLaserShots = new List<Shot>();
        activeMisShots = new List<Shot>();
        inactiveMisShots = new List<Shot>();
        for (int i = 0; i < 6; i++)
        {
            GameObject token;
            //regular shots
            token = Instantiate(regularshot,stashPos,Quaternion.identity);
            token.GetComponent<Shot>().stashList = inactiveRegShots;
            token.GetComponent<Shot>().stashPos = stashPos;
            inactiveRegShots.Add(token.GetComponent<Shot>());
            token.name += " "+i;
            //lasers
            token = Instantiate(laser, stashPos, Quaternion.identity);
            token.GetComponent<Shot>().stashList = inactiveLaserShots;
            token.GetComponent<Shot>().stashPos = stashPos;
            inactiveLaserShots.Add(token.GetComponent<Shot>());
        }
        //upwards shots
        for (int i = 0; i < 3; i++)
        {
            GameObject token;
            token = Instantiate(upwardShot, stashPos, Quaternion.identity);
            token.GetComponent<Shot>().stashList = inactiveUpShots;
            token.GetComponent<Shot>().stashPos = stashPos;
            inactiveUpShots.Add(token.GetComponent<Shot>());
        }
        //Missiles
        for (int i = 0; i < 3; i++)
        {
            GameObject token;
            token = Instantiate(missile, stashPos, Quaternion.identity);
            token.GetComponent<Shot>().stashList = inactiveMisShots;
            token.GetComponent<Shot>().stashPos = stashPos;
            inactiveMisShots.Add(token.GetComponent<Shot>());
        }

        controller = GameObject.Find("Canvas").GetComponent<UpgradeController>();
        if (controller == null)
        {
            throw new Exception("UpgradeController Object not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.paused) return;
        if (ded) return;
        if (Input.GetKeyDown(controls.a))
        {
            continuousShotDelay = 0;
            Shoot();
        }
        if(Input.GetKey(controls.a))
        {
            continuousShotDelay += Time.deltaTime;
        }
        if (continuousShotDelay >= holdFireTime)
        {
            continuousShotDelay = 0;
            Shoot();            
        }
    }

    public void Shoot()
    {
        //Regular, double and laser
        int maxactive = 2;
        if (controller.laser)
        {
            if (activeLaserShots.Count < maxactive && inactiveLaserShots.Count > 0)
            {
                shotSource.clip = laserSound;
                shotSource.Play();
                inactiveLaserShots[0].Activate(fireOriginPos.position, activeLaserShots);
            }
        }
        else
        {            
            if(controller._double)
            {
                maxactive = 1;
                if (activeUpShots.Count < maxactive && inactiveUpShots.Count > 0)
                {
                    shotSource.clip = shotSound;
                    shotSource.Play();
                    inactiveUpShots[0].Activate(fireOriginPos.position, activeUpShots);
                }
            }
            if (activeRegShots.Count < maxactive && inactiveRegShots.Count > 0)
            {
                shotSource.clip = shotSound;
                shotSource.Play();
                inactiveRegShots[0].Activate(fireOriginPos.position, activeRegShots);                
            }           
        }
        //Missile
        if(controller.missile)
        {
            maxactive = 1;
            if (activeMisShots.Count < maxactive && inactiveMisShots.Count > 0)
            {
                inactiveMisShots[0].Activate(fireOriginPos.position, activeMisShots);
            }
        }
        //Options
        for (int i = 0; i < optionsRef.Count; i++)
        {
            optionsRef[i].GetComponent<OptionBehavoir>().Shoot(controller._double, controller.laser, controller.missile);
        }
    }
}

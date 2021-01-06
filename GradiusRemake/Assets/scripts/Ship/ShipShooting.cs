using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipShooting : MonoBehaviour
{
    public List<Shot> inactiveRegShots, activeRegShots, inactiveUpShots, activeUpShots,
       inactiveLaserShots, activeLaserShots;
    public ControlsObj controls;
    protected float continuousShotDelay;//Holding fire button causes slower continuous shooting
    public float holdFireTime;

    public GameObject regularshot, upwardShot, laser;
    protected Vector3 stashPos = new Vector3(0, 100, 0);
    public Transform fireOriginPos;
    public UpgradeController controller;

    // Start is called before the first frame update
    void Awake()
    {
        activeRegShots = new List<Shot>();
        inactiveRegShots = new List<Shot>();
        activeUpShots = new List<Shot>();
        inactiveUpShots = new List<Shot>();
        activeLaserShots = new List<Shot>();
        inactiveLaserShots = new List<Shot>();
        for (int i = 0; i < 6; i++)
        {
            GameObject token;
            //regular shots
            token = Instantiate(regularshot,stashPos,Quaternion.identity);
            token.GetComponent<Shot>().stashList = inactiveRegShots;
            token.GetComponent<Shot>().stashPos = stashPos;
            inactiveRegShots.Add(token.GetComponent<Shot>());
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

        controller = GameObject.Find("Canvas").GetComponent<UpgradeController>();
        if (controller == null)
        {
            throw new Exception("UpgradeController Object not Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                    inactiveUpShots[0].Activate(fireOriginPos.position, activeUpShots);
                }
            }
            if (activeRegShots.Count < maxactive && inactiveRegShots.Count > 0)
            {
                inactiveRegShots[0].Activate(fireOriginPos.position, activeRegShots);                
            }           
        }
        
    }
}

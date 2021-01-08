using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBehavoir : MonoBehaviour
{
    public int rootMoveDelay, moveDelay;
    public List<Vector2> pastPositions;
    public List<Shot> inactiveRegShots, activeRegShots, inactiveUpShots, activeUpShots,
       inactiveLaserShots, activeLaserShots, activeMisShots, inactiveMisShots;

    protected void Start()
    {
        activeRegShots = new List<Shot>();
        activeUpShots = new List<Shot>();
        activeLaserShots = new List<Shot>();
        activeMisShots = new List<Shot>();
    }

    public void AssignInactiveLists(List<Shot> inactiveReg, List<Shot> inactiveUp, List<Shot> inactiveLaser, List<Shot> inactiveMissile)
    {
        inactiveRegShots = inactiveReg;
        inactiveUpShots = inactiveUp;
        inactiveLaserShots = inactiveLaser;
        inactiveMisShots = inactiveMissile;
    }

    public void Move()
    {
        if (pastPositions.Count < moveDelay)
        {
            this.transform.position = pastPositions[0];
        }
        else
        {
            this.transform.position = pastPositions[pastPositions.Count - moveDelay];
        }
    }

    public void Shoot(bool _double, bool laser, bool missile)
    {
        //Regular, double and laser
        int maxactive = 2;
        if (laser)
        {
            if (activeLaserShots.Count < maxactive && inactiveLaserShots.Count > 0)
            {
                inactiveLaserShots[0].Activate(transform.position, activeLaserShots);
            }
        }
        else
        {
            if (_double)
            {
                maxactive = 1;
                if (activeUpShots.Count < maxactive && inactiveUpShots.Count > 0)
                {
                    inactiveUpShots[0].Activate(transform.position, activeUpShots);
                }
            }
            if (activeRegShots.Count < maxactive && inactiveRegShots.Count > 0)
            {
                inactiveRegShots[0].Activate(transform.position, activeRegShots);
            }
        }
        //Missile
        if (missile)
        {
            maxactive = 1;
            if (activeMisShots.Count < maxactive && inactiveMisShots.Count > 0)
            {
                inactiveMisShots[0].Activate(transform.position, activeMisShots);
            }
        }

    }
}

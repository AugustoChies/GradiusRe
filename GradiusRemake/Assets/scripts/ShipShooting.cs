using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public List<Shot> inactiveShots,activeShots;
    public ControlsObj controls;
    protected float continuousShotDelay;//Holding fire button causes slower continuous shooting
    public float holdFireTime;

    public GameObject regularshot;
    protected Vector3 stashPos = new Vector3(0, 100, 0);
    public Transform fireOriginPos;
    // Start is called before the first frame update
    void Awake()
    {
        activeShots = new List<Shot>();
        inactiveShots = new List<Shot>();
        for (int i = 0; i < 6; i++)
        {
            GameObject token;
            token = Instantiate(regularshot,stashPos,Quaternion.identity);
            token.GetComponent<Shot>().stashList = inactiveShots;
            token.GetComponent<Shot>().stashPos = stashPos;
            inactiveShots.Add(token.GetComponent<Shot>());
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
        if(activeShots.Count < 2 && inactiveShots.Count > 0)
        {
            inactiveShots[0].Activate(fireOriginPos.position, activeShots);
        }
    }
}

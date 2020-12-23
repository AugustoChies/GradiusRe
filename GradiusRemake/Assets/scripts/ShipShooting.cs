using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    public List<GameObject> inactiveShots,activeShots;
    
    // Start is called before the first frame update
    void Awake()
    {
        inactiveShots = new List<GameObject>();
        activeShots = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

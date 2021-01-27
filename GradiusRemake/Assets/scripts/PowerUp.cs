using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public HazardList hazards;
    // Start is called before the first frame update
    void Awake()
    {
        hazards.misc.Add(this.gameObject);
    }

    public void Delete()
    {
        hazards.misc.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)//camborder
        {
            Delete();
        }        
        else if (col.CompareTag("Player"))
        {
            Delete();
        }
    }
}

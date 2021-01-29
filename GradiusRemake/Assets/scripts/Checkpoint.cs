using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float scrollAmount;
    public AudioClip mySong;
    //add ost here
    void Awake()
    {
        scrollAmount = Camera.main.transform.position.x - this.transform.position.x;        
    }
    
}

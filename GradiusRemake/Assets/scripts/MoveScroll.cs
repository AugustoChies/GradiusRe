using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScroll : MonoBehaviour
{
    public GlobalStats stats; 

   
    void Update()
    {
        transform.Translate(Vector2.left * stats.scrollSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScroll : MonoBehaviour
{
    public GlobalStats stats;
    public Vector2 scrollDirection = new Vector2(-1,0);

   
    void Update()
    {
        transform.Translate(scrollDirection * stats.scrollSpeed * Time.deltaTime);
    }
}

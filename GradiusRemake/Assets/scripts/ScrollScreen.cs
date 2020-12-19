using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScreen : MonoBehaviour
{
    public GlobalStats stats;
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(stats.scrollSpeed * Time.deltaTime , 0, 0);
    }
}

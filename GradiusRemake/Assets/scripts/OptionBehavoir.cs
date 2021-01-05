using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionBehavoir : MonoBehaviour
{
    public int moveDelay;
    public List<Vector2> pastPositions;

    
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

}

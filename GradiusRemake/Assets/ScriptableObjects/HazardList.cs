using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class HazardList : ScriptableObject
{
    public List<BaseEnemy> enemies;
    public List<GameObject> misc;

    public void Initialize()
    {
        enemies = new List<BaseEnemy>();
        misc = new List<GameObject>();
    }

    public void KillEnemies()
    {
        List<BaseEnemy> clone = new List<BaseEnemy>(enemies);
        for (int i = 0; i < clone.Count; i++)
        {
            clone[i].Die();
        }       
    }

    public void ClearScreen()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].gameObject != null)
                Destroy(enemies[i].gameObject);
        }
        for (int i = 0; i < misc.Count; i++)
        {
            Destroy(misc[i]);
        }
        enemies.Clear();
        misc.Clear();
    }
}

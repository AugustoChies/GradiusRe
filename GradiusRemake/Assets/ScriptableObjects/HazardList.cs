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
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Die();
        }       
    }

    public void ClearScreen()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
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

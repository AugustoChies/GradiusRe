using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GlobalStats : ScriptableObject
{
    public bool dead;
    public float scrollSpeed;
    public int playerLifes;
    public int score;
    public Vector2 playerPosition;

    public delegate void GlobalUpdate();
    public GlobalUpdate UpdateScoreEvent,UpdateLivesEvent;

    public void UpdateScore(int scoreAdd)
    {
        score += scoreAdd;
        UpdateScoreEvent();
    }

    public void UpdateLife(int lifeChange)
    {
        playerLifes += lifeChange;
        UpdateLivesEvent();
    }



}

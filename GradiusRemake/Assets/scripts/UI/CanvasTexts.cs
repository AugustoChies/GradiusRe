using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasTexts : MonoBehaviour
{
    public TextMeshProUGUI livestxt, scoretxt, highscoretxt;
    public GlobalStats stats;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLifes();
        UpdateScore();
        string tokenscoretext = "" + PlayerPrefs.GetInt("Highscore");
        int remainingCharacters = 7 - tokenscoretext.Length;
        string zeroes = "";
        for (int i = 0; i < remainingCharacters; i++)
        {
            zeroes += "0";
        }
        highscoretxt.text = zeroes + tokenscoretext;
        stats.UpdateScoreEvent += UpdateScore;
        stats.UpdateLivesEvent += UpdateLifes;
    }
    

    public void UpdateScore()
    {
        string tokenscoretext = "" + stats.score;
        int remainingCharacters = 7 - tokenscoretext.Length;
        string zeroes = "";
        for (int i = 0; i < remainingCharacters; i++)
        {
            zeroes += "0";
        }
        scoretxt.text = zeroes + tokenscoretext;
    }

    public void UpdateLifes()
    {
        livestxt.text = "";
        if (stats.playerLifes > 0)
            livestxt.text += stats.playerLifes;
    }
}

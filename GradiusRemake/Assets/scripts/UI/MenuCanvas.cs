using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuCanvas : MonoBehaviour
{
    public GlobalStats stats;
    public ControlsObj controls;
    protected bool moving,starting;
    public float moveSpeed;
    public RectTransform rect;
    public TextMeshProUGUI playerText, highScoreText;
    public GameObject cursor;
    public float startWaitTime, cursorBlinkTime;
    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        string tokenscoretext = "" + PlayerPrefs.GetInt("Highscore");
        int remainingCharacters = 7 - tokenscoretext.Length;
        string zeroes = "";
        for (int i = 0; i < remainingCharacters; i++)
        {
            zeroes += "0";
        }         
        highScoreText.text = "HI " + zeroes + tokenscoretext;
    }

    // Update is called once per frame
    void Update()
    {
        if (starting) return;

        if (moving)
        {
            rect.anchoredPosition += Vector2.right * moveSpeed * Time.deltaTime;
            if(Input.GetKeyDown(controls.start))
            {
                rect.anchoredPosition = Vector2.zero;
                moving = false;
                cursor.SetActive(true);
                return;
            }


            if(rect.anchoredPosition.x >= 0.5f)
            {
                rect.anchoredPosition = Vector2.zero;
                moving = false;
                cursor.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(controls.start))
            {
                starting = true;
                StartCoroutine(StartSequence());
            }
        }
    }

    IEnumerator StartSequence()
    {
        //playsound
        StartCoroutine(TextBlink());
        yield return new WaitForSeconds(startWaitTime);
        stats.playerLifes = 3;
        stats.score = 0;
        SceneManager.LoadSceneAsync("Stage");
    }

    IEnumerator TextBlink()
    {
        playerText.enabled = !playerText.enabled;
        yield return new WaitForSeconds(cursorBlinkTime);
        StartCoroutine(TextBlink());
    }
}

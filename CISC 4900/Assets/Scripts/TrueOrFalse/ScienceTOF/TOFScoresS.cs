using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TOFScoresS : MonoBehaviour
{
/**
 * TOFScores handles the scoring system in the True or False quiz
 * 
 * Start sets the current score to 0 on startup
 * AddScore adds plus 10 to the currentscore when the user answers correctly
 * MinusScore subtracts 10 from the score until it reaches 0
 * */



    public TextMeshProUGUI  scoreText;
    private int currentScore;

    void Start()
    {
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    public void AddScore()
    {
        currentScore += 10;
        scoreText.text = currentScore.ToString();
    }
    
    public void MinusScore()
    {
        currentScore = currentScore > 0 ? currentScore - 10 : 0;
        scoreText.text = currentScore.ToString();
    }
}

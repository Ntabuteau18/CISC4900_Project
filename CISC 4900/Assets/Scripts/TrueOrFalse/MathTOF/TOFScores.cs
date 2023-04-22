using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/**
 * Standard Scoring system that starts at 0 and if its correct it adds 10 and if incorrect it substracts 10 until
 * it reaches zero then it stays at zero.
 * */
public class TOFScores : MonoBehaviour
{

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

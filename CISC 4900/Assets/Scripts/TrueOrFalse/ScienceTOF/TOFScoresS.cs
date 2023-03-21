using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TOFScoresS : MonoBehaviour
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

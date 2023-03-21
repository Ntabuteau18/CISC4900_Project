using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TOFQuestionDataG : MonoBehaviour
{
    public ToFQuestionsG questions;
    [SerializeField] private TextMeshProUGUI questionText;

    public Canvas gameOverCanvas; // Reference to the canvas that displays the "Game Over" message
    public TextMeshProUGUI gameOverText; // Reference to the Text component that displays the "Game Over" message

    void Start()
    {
        AskQuestion();
    }

    public void AskQuestion()
    {
        if (CountRemainingQuestions() == 0)
        {
           questionText.text = string.Empty;
            ClearQuestions();
          //  SceneManager.LoadScene(0);
            return;
        }

        var randomIndex = 0;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, questions.questionlist.Count);
        } while (questions.questionlist[randomIndex].questioned == true);

        questions.currentQuestion = randomIndex;
        questions.questionlist[questions.currentQuestion].questioned = true;
        questionText.text = questions.questionlist[questions.currentQuestion].question;
    }

    public void ClearQuestions()
    {
        foreach (var question in questions.questionlist)
        {
            question.questioned = false;
        }
    }

    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(0);
    }



public int CountRemainingQuestions()
{
    int remainingQuestions = 0;

    foreach (var question in questions.questionlist)
    {
        if (question.questioned == false)
            remainingQuestions++;
    }

    Debug.Log("Questions Left " + remainingQuestions);

    if (remainingQuestions == 0)
    {
        // Show game over message
        gameOverCanvas.gameObject.SetActive(true); // Show the canvas
        gameOverText.text = "Game Over"; // Set the text of the Text component to "Game Over"
    }

    return remainingQuestions;
}


}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TOFQuestionDataG : MonoBehaviour
{
    /**
     * TOFQuesetionData is the main game in asking users the question and handling setting questions to done after they have been answered
     * 
     * AskQuestion selects a random question to ask to the user from a random index and after asking sets the status of the question to questioned
     * ClearQuestions sets all questions status to unasked when the game starts up so all are ready to be loaded in
     * Menu takes the user back to the menu
     * CountRemainingQuestions counts how many questions are left and when over displays a game over message and a menu button
     * */
    public ToFQuestionsG questions;
    [SerializeField] private TextMeshProUGUI questionText;

    public Canvas gameOverCanvas; 
    public TextMeshProUGUI gameOverText; 

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
        gameOverCanvas.gameObject.SetActive(true); 
        gameOverText.text = "Game Over"; 
    }

    return remainingQuestions;
}


}


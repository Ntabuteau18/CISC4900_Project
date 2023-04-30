using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GeographyImageQuizManager : MonoBehaviour
{
    /**
    * ImageQuizManager is the script that handles all the mechanics from the game such as setting answers and questions, and setting correct or wrong answers
    * Start sets the gameover screen to false, creates a count of all questions in the list and creates a question for the user
    * Correct adds 10 to the score, removes the current question from the list and sets a delay before the next question shows
    * Wrong removes the current question from the list and sets a delay before the next question shows
    * GameOver sets the game over screen to true, displaying the users final score
    * SetAnswers sets all options correctness to false initially then sets the text of each option to its corresponding answer, finally it finds 
    * the correct answer choice by using the number index from the "CorrectAnswer" field of the current question
    * Restart restarts from the beginning
    * Menu goes back to the menu
    * IEnumerator Delay is a delay for a second before the next question displays
    * CreateQuestion gets a random question and image from the list and displays it
    * 
    * */
    public List<GeographyImageQnA> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Image QuestionImage; 
    public TMP_Text ScoreText;

    public TMP_Text QuestionText;

    int totalQuestions = 0;
    public int score;

    public GameObject QuizPanel;
    public GameObject GameOverPanel;

    void Start()
    {
        totalQuestions = QnA.Count;
        GameOverPanel.SetActive(false);
        CreateQuestion();
    }

    public void correct()
    {
        score += 10;
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(Delay());
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(Delay());
    }

    public void GameOver()
    {
        QuizPanel.SetActive(false);
        GameOverPanel.SetActive(true);
        ScoreText.text = score.ToString();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<GeographyImageAnswers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<GeographyImageAnswers>().isCorrect = true;
            }
        }
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f); 
        CreateQuestion();
    }

    void CreateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            QuestionImage.sprite = QnA[currentQuestion].QuestionImage;
            QuestionText.text = QnA[currentQuestion].QuestionText;

            SetAnswers();
        }
        else
        {
            GameOver();
        }
    }

}








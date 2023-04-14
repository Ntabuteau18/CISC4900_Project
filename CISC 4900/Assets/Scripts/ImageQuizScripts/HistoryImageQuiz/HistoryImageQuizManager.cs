using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HistoryImageQuizManager : MonoBehaviour
{
    public List<HistoryImageQnA> QnA;
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
            options[i].GetComponent<HistoryImageAnswers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<HistoryImageAnswers>().isCorrect = true;
            }
        }
    }

    // retstart the scene
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // go to the menu
    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f); // delay for 1 second before creating next question
        CreateQuestion();
    }

    // get a random question and display it
    void CreateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            // Load image from the QnA list and assign to the QuestionImage sprite
            QuestionImage.sprite = QnA[currentQuestion].QuestionImage;

            // Load text question from the QnA list and assign to the QuestionText component
            QuestionText.text = QnA[currentQuestion].QuestionText;

            SetAnswers();
        }
        else
        {
            Debug.Log("finished");
            GameOver();
        }
    }

}








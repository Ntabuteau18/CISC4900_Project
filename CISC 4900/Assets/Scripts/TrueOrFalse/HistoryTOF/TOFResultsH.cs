using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TOFResultsH : MonoBehaviour
{
    public ToFQuestionsH questions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;

    public TOFScoresH scores;

    public Button trueButton;
    public Button falseButton;

    public UnityEvent onNextQuestion;

    private TOFQuestionDataH questionData;

    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    void Start()
    {
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        // Get reference to the TOFQuestionData script
        questionData = FindObjectOfType<TOFQuestionDataH>();

        // Disable buttons if there are no remaining questions
        if (questionData.CountRemainingQuestions() == 0)
        {
            trueButton.interactable = false;
            falseButton.interactable = false;
        }
    }

    public void ShowResults(bool answer)
    {
        correctSprite.SetActive(questions.questionlist[questions.currentQuestion].isTrue == answer);
        incorrectSprite.SetActive(questions.questionlist[questions.currentQuestion].isTrue != answer);

        if (questions.questionlist[questions.currentQuestion].isTrue == answer)
        {
            scores.AddScore();
            audioSource.PlayOneShot(correctSound);
        }
        else
        {
            scores.MinusScore();
            audioSource.PlayOneShot(incorrectSound);
        }

        // Disable buttons if there are no remaining questions
        if (questionData.CountRemainingQuestions() == 0)
        {
            trueButton.interactable = false;
            falseButton.interactable = false;
        }
        else
        {
            trueButton.interactable = true;
            falseButton.interactable = true;
        }

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1f);
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        onNextQuestion.Invoke();

    }
}

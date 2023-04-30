using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TOFResultsS : MonoBehaviour
{
    /**
     * TOFResults handles the submission data the user has chosen and gives them feedback in the form of the answer being right or wrong
     * 
     * Start sets the true and false buttons to false initially and when the quiz is over turns them off
     * ShowResults handles the showing the correct or incorrect sprite after an answer is made as well as a sound effect
     * IEnumerator ShowResult makes the sprites wait a second before dissapearing when the next question starts
     * 
     */
    public ToFQuestionsS questions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;

    public TOFScoresS scores;

    public Button trueButton;
    public Button falseButton;

    public UnityEvent onNextQuestion;

    private TOFQuestionDataS questionData;

    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    void Start()
    {
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        
        questionData = FindObjectOfType<TOFQuestionDataS>();

        
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

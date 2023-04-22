using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/**
 * This is the results script that handles everything once the player has submitted an answer it starts by when a 
 * answer is selected it shows the corresponding sprite to say if its right or wrong. It then either adds or subtracts the
 * score and plays the corresponding sound effect to tell the user if they answered correctly or not. There is a delay in place
 * that makes the user wait between questions for a second to not accidentally answer rapidly. Once all questoins are done
 * the game will be over and show a game over pop up and disable the functionality of the true and false buttons.
 */
public class TOFResults : MonoBehaviour
{
    public ToFQuestions questions;
    public GameObject correctSprite;
    public GameObject incorrectSprite;

    public TOFScores scores;

    public Button trueButton;
    public Button falseButton;

    public UnityEvent onNextQuestion;

    private TOFQuestionData questionData;

    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    void Start()
    {
        correctSprite.SetActive(false);
        incorrectSprite.SetActive(false);

        // Get reference to the TOFQuestionData script
        questionData = FindObjectOfType<TOFQuestionData>();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeographyImageAnswers : MonoBehaviour
{
    /*
     * ImageAnswers handles the process of what happens when an answer is submitted by giving the corresponding feedback to the user if right or wrong
     * 
     * Answer is a method that sets the answer selected by the user to green if correct and red if wrong while playing a sound effect
     * IEnumerator ResetColorAfterDelay sets the color back to its normal color after 1 second
     * 
     * */
    public bool isCorrect = false;
    public GeographyImageQuizManager quizManager;

    public AudioSource audioSource;
    public AudioClip correctAudioSource;
    public AudioClip wrongAudioSource;

    public Color starterColor;

    private void Start()
    {
        starterColor = GetComponent<Image>().color;
    }

    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            Debug.Log("Correct");
            quizManager.correct();

            audioSource.PlayOneShot(correctAudioSource);
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            Debug.Log("Wrong");
            quizManager.wrong();
            audioSource.PlayOneShot(wrongAudioSource);
        }

        
        StartCoroutine(ResetColorAfterDelay(1f));
    }

    private IEnumerator ResetColorAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        GetComponent<Image>().color = starterColor;
    }
}


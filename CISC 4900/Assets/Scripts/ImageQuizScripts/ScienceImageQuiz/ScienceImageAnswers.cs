using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScienceImageAnswers : MonoBehaviour
{
    public bool isCorrect = false;
    public ScienceImageQuizManager quizManager;

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

            if (wrongAudioSource != null)
            {
                audioSource.PlayOneShot(wrongAudioSource);
            }
        }

        // Set the color back to the starter color after 1 second
        StartCoroutine(ResetColorAfterDelay(1f));
    }

    private IEnumerator ResetColorAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        GetComponent<Image>().color = starterColor;
    }
}


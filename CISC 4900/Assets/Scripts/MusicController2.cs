using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController2 : MonoBehaviour
{
    /**
     * Music Controller is the central audio system in the game that handles how the music is played throughout the game.
     * It first makes instance variable that makes sure that only 1 object of it exists at all times and is not destroyed
     * as the scene changes, then we attach an audioSource component to a variable. On Start the first music clip is played
     * and on Update if the name of the scene matches one of the categories, a new music clip is played and the previous one
     * stops. This repeats for the entire game and is simply a switching of music depending on the scene name
     * */

    public static MusicController2 instance;
    public AudioClip musicClip1;
    public AudioClip musicClip2;
    public AudioClip musicClip3;
    public AudioClip musicClip4;

    private AudioSource audioSource;
    private AudioClip currentClip;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentClip = musicClip1;
        audioSource.clip = currentClip;
        audioSource.Play();
    }

    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MathQuiz" || sceneName == "HistoryQuiz" || sceneName == "ScienceQuiz")
        {
            if (currentClip != musicClip2)
            {
                audioSource.Pause();
                currentClip = musicClip2;
                audioSource.clip = currentClip;
                audioSource.Play();
            }
        }
        else if (sceneName == "TrueOrFalseHistory" || sceneName == "TrueOrFalseMath" || sceneName == "TrueOrFalseScience" || sceneName == "TrueOrFalseGeography")
        {
            if (currentClip != musicClip3)
            {
                audioSource.Pause();
                currentClip = musicClip3;
                audioSource.clip = currentClip;
                audioSource.Play();
            }
        }
        else if (sceneName == "MathImageQuiz" || sceneName == "HistoryImageQuiz" || sceneName == "ScienceImageQuiz" || sceneName == "GeographyImageQuiz")
        {
            if (currentClip != musicClip4)
            {
                audioSource.Pause();
                currentClip = musicClip4;
                audioSource.clip = currentClip;
                audioSource.Play();
            }
        }
        else
        {
            if (currentClip != musicClip1)
            {
                audioSource.Pause();
                currentClip = musicClip1;
                audioSource.clip = currentClip;
                audioSource.Play();
            }
        }
    }

}



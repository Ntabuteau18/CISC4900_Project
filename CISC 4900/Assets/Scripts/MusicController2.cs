using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController2 : MonoBehaviour
{
    public static MusicController2 instance;

    public AudioClip musicClip1;
    public AudioClip musicClip2;

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
        if (sceneName == "QuizBeginning" || sceneName == "HistoryQuiz")
        {
            if (currentClip != musicClip2)
            {
                audioSource.Pause();
                currentClip = musicClip2;
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioClip musicClip1;
    public AudioClip musicClip2;

    private AudioSource audioSource;
 

    public static MusicController instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

  

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "QuizBeginning")
        {
            MusicController.instance.GetComponent<AudioSource>().Pause();
            audioSource.clip = musicClip2;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = musicClip1;
            audioSource.Play();
        }


    }
}


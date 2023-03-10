using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
   
    private int sceneIndex = 0;
    private AudioSource music;

    private void Awake()
    {
        GameObject[] musicObject = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObject.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        music = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex == 2)
            {
                music.Stop();
            }
        }
    }
}


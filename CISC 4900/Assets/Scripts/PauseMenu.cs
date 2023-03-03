using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public GameObject pauseMenu; 
   public static bool isPaused = false;


    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){

                Resume();

            }else{

                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void loadMenu(){
        Debug.Log("Going to Menu");
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

}

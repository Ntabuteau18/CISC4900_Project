using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Main Menu Script a simple script that only contains the option for a player to quit the game when done
 * and press play which forwards the player to the next scene which is the selection screen.
 * 
 * **/
public class MainMenu : MonoBehaviour
{

    
    // Load Scene
    public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit Game
    public void Quit(){
        Application.Quit();
        Debug.Log("Player has Quit the Game");
    }

    

}

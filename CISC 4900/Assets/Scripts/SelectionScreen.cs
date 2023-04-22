using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This is the selection screen settings that takes the input from the user and loads the corresponding scene.
 * Each name indicates what scene the user will be sent to.
 * */
public class SelectionScreen : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToMathScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToHistoryQuizScene()
    {
        SceneManager.LoadScene(3);
    }

    public void GoToMathTrueOrFalseQuiz()
    {
        SceneManager.LoadScene(4);
    }

    public void GoToHistoryTrueOrFalse()
    {
        SceneManager.LoadScene(5);
    }

    public void GoToScienceTrueOrFalse()
    {
        SceneManager.LoadScene(6);
    }

    public void GoToGeographyTrueOrFalse()
    {
        SceneManager.LoadScene(7);
    }

    public void GoToScienceQuizScene()
    {
        SceneManager.LoadScene(8);
    }

    public void GoToMathImageQuiz()
    {
        SceneManager.LoadScene(9);
    }

    public void GoToHistoryImageQuiz()
    {
        SceneManager.LoadScene(10);
    }

    public void GoToScienceImageQuiz()
    {
        SceneManager.LoadScene(11);
    }

    public void GoToGeographyImageQuiz()
    {
        SceneManager.LoadScene(12);
    }

}

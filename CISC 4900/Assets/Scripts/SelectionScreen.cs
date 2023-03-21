using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


}

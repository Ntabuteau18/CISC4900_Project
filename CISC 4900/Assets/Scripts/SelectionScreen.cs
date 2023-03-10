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

    public void GoToScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GoToHistoryQuizScene()
    {
        SceneManager.LoadScene(3);
    }

}

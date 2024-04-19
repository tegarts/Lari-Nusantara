using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Gameplay 1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void loadGame(){

        SceneManager.LoadScene("Main");

    }
    public void quitGame(){

        Application.Quit();

    }
    public void restartGame(){

        SceneManager.LoadScene("Main");
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ExitGame()
    {
        Application.Quit();
    }
    void ExitGameKey()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}

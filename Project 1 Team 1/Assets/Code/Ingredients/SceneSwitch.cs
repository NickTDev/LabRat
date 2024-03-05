using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void buttonMoveScene(string sceneName)
    {
        if (sceneName != "mainMenu")
            Cursor.visible = false;
        else
            Cursor.visible = true;

        SceneManager.LoadScene(sceneName);
    }
    public void quit()
    {
        Application.Quit();
    }
}
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    public static event Action OnGameReset;

    public string resetSceneName = "StartScreen";
    public bool escapeResets;

    private void Awake()
    {
        if (!CompareTag("GameInput"))
            Debug.LogError("Invalid tag! Must be \"GameInput\"\n");

        DontDestroyOnLoad(gameObject);

        GameObject[] gameInputs = GameObject.FindGameObjectsWithTag("GameInput");
        if (gameInputs.Length > 1)
            Destroy(gameInputs[1]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escapeResets && SceneManager.GetActiveScene().name != resetSceneName)
                ResetGame();
            else
                ExitGame();
        }
        if (!escapeResets && Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        OnGameReset?.Invoke();
        Cursor.visible = true;
        SceneManager.LoadScene(resetSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting! ");
        Application.Quit();
    }
}

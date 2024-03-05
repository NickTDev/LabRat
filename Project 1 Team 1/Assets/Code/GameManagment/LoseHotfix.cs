using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseHotfix : MonoBehaviour
{
    private void OnEnable()
    {
        TimerSystem.OnTimerOver += OnTimerOver;
    }
    private void OnDisable()
    {
        TimerSystem.OnTimerOver -= OnTimerOver;
    }

    private void OnTimerOver()
    {
        SceneManager.LoadScene("Lose");
    }
}

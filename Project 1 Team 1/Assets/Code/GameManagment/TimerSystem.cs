using System;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    public static event Action OnTimerOver;

    static TimerSystem inst; // Singleton Structure 

    private void Awake()
    {
        inst = this;
    }

    public static bool GetTimerRunning { get => inst.timerIsRunning; }
    public static float GetTimeRemaining { get => inst.timeRemaining; } // This is a property (Basically C#'s special getters and setters) 
    public static float GetTimeSet { get => inst.timeSet; }
    public static float GetPercentTime { get => inst.timeRemaining / inst.timeSet; }

    private float timeRemaining;
    private float timeSet;
    private bool timerIsRunning = false;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                OnTimerOver?.Invoke(); // A game manager should listen to this event and send to a lose screen 
                // Event is used bc this timer don't care about what to do when it is done 

                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public static void SetTimer(float time)
    {
        inst.timeRemaining = time;
        inst.timeSet = time;
        inst.timerIsRunning = true;
    }

    public static void PauseTimer()
    {
        inst.timerIsRunning = false;
    }

    public static void ResumeTimer()
    {
        inst.timerIsRunning = true;
    }
}

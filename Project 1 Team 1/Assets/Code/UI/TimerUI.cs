using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Image fillImage;

    private void Update()
    {
        if (TimerSystem.GetTimerRunning)
        {
            fillImage.fillAmount = TimerSystem.GetPercentTime;
        }
        else
        {
            fillImage.fillAmount = 1;
        }
    }
}

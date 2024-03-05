using UnityEngine;

public class TutorialWon : MonoBehaviour
{
    [SerializeField] CanvasGroup winGroup;
    [Tooltip("alpha per sec ")]
    [SerializeField] float showSpeed = 1f/2f;
    bool showGroup = false;

    private void OnEnable()
    {
        Deposit.OnNoMoreOrders += OnNoMoreOrders;
    }

    private void OnDisable()
    {
        Deposit.OnNoMoreOrders -= OnNoMoreOrders;
    }

    private void Update()
    {
        if (showGroup)
        {
            winGroup.alpha += showSpeed * Time.deltaTime;
        }
    }

    public void OnNoMoreOrders()
    {
        showGroup = true;
    }
}

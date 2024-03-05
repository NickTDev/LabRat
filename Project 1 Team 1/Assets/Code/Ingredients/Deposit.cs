using System;
using UnityEngine;

public enum Difficulty
{
    NONE = -1,
    easy,
    medium,
    hard,
    MAX
}

public class Deposit : MonoBehaviour
{
    public static event Action OnNoMoreOrders;
    public static event Action<Sprite> OnIngredientAdded;
    public static event Action OnOrderComplete;

    public Order[] EasyOrders;
    public Order[] MediumOrders;
    public Order[] HardOrders;

    [Tooltip("Number of easy orders that must be complete before a medium one is picked")]
    public int numEasyOrdersToComplete = 2;
    [Tooltip("Number of medium orders that must be complete before a hard one is picked")]
    public int numMediumOrdersToComplete = 3;
    [Tooltip("Number of hard orders that must be complete before you win!")]
    public int numHardOrdersToComplete = 5;

    Order currentOrder;
    int ordersCompleted = 0;
    bool hasOrder = false;
    bool noMoreOrders = false;

    int itemsInPot = 0;

    public Order GetCurrentOrder { get => currentOrder; }
    public bool GetHasOrder { get => hasOrder; }
    public int GetItemsInPot { get => itemsInPot; }
    public Ingredient GetCurrentIngrient { get => currentOrder[itemsInPot]; }

    void Update()
    {
        if (!hasOrder && !noMoreOrders)
        {
            // Figure out difficulty 
            Difficulty difficulty = Difficulty.NONE;

            if (ordersCompleted < numEasyOrdersToComplete)
                difficulty = Difficulty.easy;
            else if (ordersCompleted < numEasyOrdersToComplete + numMediumOrdersToComplete)
                difficulty = Difficulty.medium;
            else if (ordersCompleted < numEasyOrdersToComplete + numMediumOrdersToComplete + numHardOrdersToComplete)
                difficulty = Difficulty.hard;
            else if (!noMoreOrders) // We Win! 
            {
                OnNoMoreOrders?.Invoke();
                noMoreOrders = true;
                return;
            }

            currentOrder = ChooseOrder(difficulty);
            Debug.Log(currentOrder);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (the PlayerMovement component does not exist) 
        if (!collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            return; // Don't do anything if it was not the player that collided 
        }

        if (hasOrder)
        {
            if (currentOrder[itemsInPot] != null)
            {
                if (currentOrder[itemsInPot] == player.heldIngredient)
                {
                    // Deposit! 
                    Debug.Log("Deposited " + player.heldIngredient.prefabName);

                    OnIngredientAdded?.Invoke(player.heldIngredient.ingredientSprite); // To be used in the kaploop of the ingredient 

                    player.DropIngredient();
                    itemsInPot++;

                    // Check to see if the order is done 
                    if (itemsInPot >= currentOrder.Length)
                    {
                        TimerSystem.PauseTimer();
                        itemsInPot = 0;
                        hasOrder = false;

                        ordersCompleted++;
                        OnOrderComplete?.Invoke(); // No idea if this'll be used but it seems important to have 
                        Debug.Log($"Completed Orders: {ordersCompleted}");
                    }
                }
            }
        }
    }

    Order ChooseOrder(Difficulty difficulty)
    {
        Order result;

        // Pick the order based on difficulty given 
        switch (difficulty)
        {
            case Difficulty.easy:
                result = EasyOrders[UnityEngine.Random.Range(0, EasyOrders.Length)]; // Pick a random order 
                break;
            case Difficulty.medium:
                result = MediumOrders[UnityEngine.Random.Range(0, MediumOrders.Length)]; // Pick a random order 
                break;
            case Difficulty.hard:
                result = HardOrders[UnityEngine.Random.Range(0, HardOrders.Length)]; // Pick a random order 
                break;
            default:
                throw new System.Exception("Could not choose order! ");
        }

        // Set things that need to be set 
        hasOrder = true;
        TimerSystem.SetTimer(result.GetTimeToComplete);

        return result;
    }
}

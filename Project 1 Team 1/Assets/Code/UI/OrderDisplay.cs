using System;
using UnityEngine;
using UnityEngine.UI;

public class OrderDisplay : MonoBehaviour
{
    [SerializeField] private Image[] ingredientImages;
    [SerializeField] private Sprite noIngredient;

    private Deposit deposit;

    private void Awake()
    {
        // Cache deposit ref 
        deposit = GameObject.FindGameObjectWithTag("Deposit").GetComponent<Deposit>();
        Debug.Assert(deposit != null, "Could not find deposit! ");
    }

    private void Update()
    {
        // Clear the images if there is no order 
        if (!deposit.GetHasOrder)
        {
            foreach (Image i in ingredientImages)
                i.sprite = noIngredient;

            return; // Don't continue 
        }

        // An order is guarenteed from above code 
        for (int i = 0; i < ingredientImages.Length; i++)
        {
            // Fill in ingredient sprites and if there are no more then set to null 
            try
            {
                ingredientImages[i].sprite = deposit.GetCurrentOrder[i + deposit.GetItemsInPot].ingredientSprite;
            }
            catch (IndexOutOfRangeException)
            {
                ingredientImages[i].sprite = noIngredient;
            }
        }
    }
}

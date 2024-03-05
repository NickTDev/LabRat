using System.Collections.Generic;
using UnityEngine;

public class IngredientCompass : MonoBehaviour
{
    private Deposit deposit;

    private GameObject arrow;
    private PlayerMovement player;

    private string activeIngredientName = "";
    private Transform activeTransform;
    private Dictionary<string, Transform> ingredientLocations;

    private void Awake()
    {
        deposit = GameObject.Find("Pot").GetComponent<Deposit>();
        Debug.Assert(deposit != null, "deposit is null!");

        arrow = transform.GetChild(0).gameObject;
        Debug.Assert(arrow.name == "Arrow", "arror was not found correctly! Make sure it is named \"Arrow\" in the heirarchy ");

        player = transform.parent.GetComponent<PlayerMovement>();

        /* Fill Dictionary */
        // Oh god this is going to suck 
        // Basic idea: Find all the ingredients, get their prefabName, use that as a key for a dictionary 
        GameObject[] ingredients = GameObject.FindGameObjectsWithTag("Ingredient");

        // Check check for sanity 
        if (ingredients.Length != 5)
            Debug.LogWarning($"Expected 5 ingredients but found {ingredients.Length}");

        // Create and fill the dictionary 
        ingredientLocations = new Dictionary<string, Transform>();
        foreach (GameObject go in ingredients)
        {
            IngredientManager manager = go.GetComponent<IngredientManager>();
            Debug.Assert(manager != null);

            // Store the transform with the key of prefabName 
            ingredientLocations[manager.ingredientValues.prefabName] = go.transform;
        }
    }

    private void Update()
    {
        if (player == null || player.heldIngredient == null)
            return;

        // Only display if there is an order and the player does not have the ingredient 
        bool playerHasActiveIngredient = player.heldIngredient.prefabName == activeIngredientName;
        arrow.SetActive(deposit.GetHasOrder && !playerHasActiveIngredient);

        if (deposit.GetHasOrder)
        {
            // Cache the name so that we are not searching a dictionary every frame 
            if (activeIngredientName != deposit.GetCurrentIngrient.prefabName)
            {
                activeIngredientName = deposit.GetCurrentIngrient.prefabName;
                activeTransform = ingredientLocations[deposit.GetCurrentIngrient.prefabName]; // Get location based on name 
            }

            // Get normalized vector pointing from the player to the active ingredient 
            Vector2 toIngredient = activeTransform.position - transform.position;
            toIngredient.Normalize();

            // Make the x axis of this object (parent to the arrow) towards the ingredient 
            // This will rotate the arrow to point in that direction while being a set distance from the player 
            transform.right = toIngredient;
        }
    }
}

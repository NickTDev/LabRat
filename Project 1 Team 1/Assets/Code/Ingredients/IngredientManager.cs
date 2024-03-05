using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    //public GameObject ingredientToSpawn;

    public Ingredient ingredientValues;

    // Start is called before the first frame update
    void Start()
    {
        SpawnIngredient();
    }

    void SpawnIngredient()
    {
        //GameObject currentIngredient = Instantiate(ingredientToSpawn, ingredientValues.spawnPoint[0], Quaternion.identity);

        //currentIngredient.name = ingredientValues.prefabName;
    }
}

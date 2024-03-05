using UnityEngine;

public class IngredientSprite : MonoBehaviour
{
    public Sprite defaultSprite;

    private void OnValidate()
    {
        SetSprite();
    }

    private void Awake()
    {
        SetSprite();
    }

    void SetSprite()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        IngredientManager manager = GetComponent<IngredientManager>();

        Ingredient ingredient = manager.ingredientValues;

        if (ingredient == null)
        {
            rend.sprite = defaultSprite;
        }
        else
        {
            rend.sprite = ingredient.ingredientSprite;
        }
    }
}

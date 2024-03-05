using UnityEngine;

[CreateAssetMenu]
public class Order : ScriptableObject
{
    [SerializeField] private Ingredient[] ingredients;
    [SerializeField] private float timeToComplete = 300;

    [Tooltip("This is only a label and does not do anything ")]
    public Difficulty difficulty = new Difficulty();

    /// <summary>
    /// Returns the ingredient at that index. Just passes responsibility. 
    /// </summary>
    public Ingredient this[int i] { get => ingredients[i]; }
    public float GetTimeToComplete { get => timeToComplete; }
    public int Length { get => ingredients.Length; } // This also just passes responsibilities but hey 

    // Makes it easier to print for debug purposes 
    public override string ToString()
    {
        if (ingredients == null || ingredients.Length <= 0)
            return "Empty Order";

        string s = $"Difficulty: {difficulty}, Ingredients: {ingredients[0]}";

        for (int i = 1; i < ingredients.Length; i++)
        {
            s += $", {ingredients[i].prefabName}";
        }

        return s;
    }
}

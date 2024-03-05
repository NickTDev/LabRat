using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredients")]
public class Ingredient : ScriptableObject
{
    public string prefabName = "None";

    public float jumpHeight = 3.5f;
    public float moveSpeed = 7;
    public int jumps = 1;
    public float acceleration = 14;
    public float decceleration = 8;
    public float timeMod = 1;
    public float gravityScale = 1;

    [HideInInspector] public bool isHeld = false;

    public Sprite ingredientSprite;
}

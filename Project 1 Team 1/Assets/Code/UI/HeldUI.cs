using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeldUI : MonoBehaviour
{
    public string currentItemName;

    public Text heldText;

    GameObject playerIngredient;
    PlayerMovement player;

    // Update is called once per frame
    void Update()
    {
        displayOrder();
    }

    void displayOrder()
    {
        playerIngredient = GameObject.Find("Player");
        player = playerIngredient.GetComponent<PlayerMovement>();

        heldText.text = player.heldIngredient.prefabName;
    }
}

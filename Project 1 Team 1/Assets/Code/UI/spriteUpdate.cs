using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteUpdate : MonoBehaviour
{

    public Sprite[] tables;
    private Deposit deposit;
    private SpriteRenderer spriteRenderer;
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        deposit = GetComponent<Deposit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteChange();
    }

    void spriteChange()
    {
        if(!deposit.GetHasOrder)
        {
            return;
        }

        int i = deposit.GetItemsInPot;
        int n = deposit.GetCurrentOrder.Length;

        float precent = (float)i / n;

        int index = (int)(precent * tables.Length);
        sprite = tables[index];

        spriteRenderer.sprite = sprite;
    }
}

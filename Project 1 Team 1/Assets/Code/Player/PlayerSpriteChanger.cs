using UnityEngine;

public class PlayerSpriteChanger : MonoBehaviour
{
    public Sprite crouchSprite;
    public Sprite standSprite;

    SpriteRenderer rend;
    Rigidbody2D rb;
    PlayerMovement player;

    float direction;

    private void Awake()
    {
        //player = transform.parent.GetComponent<PlayerMovement>();
        //rend = GetComponent<SpriteRenderer>();
        rb = transform.parent.GetComponent<Rigidbody2D>();

        direction = 1;
    }

    private void Update()
    {
        //if (player.IsCrouch)
        //{
        //    rend.sprite = crouchSprite;
        //}
        //else
        //{
        //    rend.sprite = standSprite;
        //}

        // Flip sprite based on movement 
        if (Mathf.Abs(rb.velocity.x) > 0.1f) // Avoid floating point issues 
        {
            float sign = Mathf.Sign(rb.velocity.x);
            direction = sign != 0 ? sign : direction; // Make sure our scale is never 0 
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }
}

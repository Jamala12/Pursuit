using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float MovementSpeed;
    private float InputHorizontal;
    private float InputVertical;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        InputHorizontal = Input.GetAxisRaw("Horizontal");
        InputVertical = Input.GetAxisRaw("Vertical");

        if (InputHorizontal < 0)
        {
            spriteRenderer.flipX = true; // Flip sprite to left
        }
        else if (InputHorizontal > 0)
        {
            spriteRenderer.flipX = false; 
        }
    }

    void FixedUpdate()
    {
        if (InputHorizontal != 0 || InputVertical != 0)
        {
            if (InputHorizontal != 0 && InputVertical != 0) // If the input is diagonal --> Normalize
            {
                rb.velocity = new Vector2(InputHorizontal * MovementSpeed, InputVertical * MovementSpeed).normalized * MovementSpeed;
            }
            else // If the input is not diagonal
            {
                rb.velocity = new Vector2(InputHorizontal * MovementSpeed, InputVertical * MovementSpeed);
            }
        }
        else // If there is no movement input set velocity to zero
        {
            rb.velocity = Vector2.zero;
        }
    }
}

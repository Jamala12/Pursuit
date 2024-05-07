using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] 
    private float movementSpeed;
    private float InputHorizontal;
    private float InputVertical;
    private SpriteRenderer spriteRenderer;
    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        LoadMovementSpeed();
    }

    private void LoadMovementSpeed()
    {
        LoadCharacter loader = FindObjectOfType<LoadCharacter>();
        if (loader != null)
        {
            movementSpeed = loader.GetMovementSpeed();
        }
        else
        {
            Debug.LogError("LoadCharacter script not found in the scene.");
        }
    }

    void Update()
    {
        InputHorizontal = Input.GetAxisRaw("Horizontal");
        InputVertical = Input.GetAxisRaw("Vertical");

        if (InputHorizontal < 0)
        {
            spriteRenderer.flipX = true;
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
                rb.velocity = new Vector2(InputHorizontal * movementSpeed, InputVertical * movementSpeed).normalized * movementSpeed;
            }
            else // If the input is not diagonal
            {
                rb.velocity = new Vector2(InputHorizontal * movementSpeed, InputVertical * movementSpeed);
            }
        }
        else // If there is no movement input set velocity to zero
        {
            rb.velocity = Vector2.zero;
        }
    }
    public void ApplySpeedBoost(float multiplier, float boostDuration)
    {
        StartCoroutine(BoostSpeed(multiplier, boostDuration));
    }

    private IEnumerator BoostSpeed(float multiplier, float duration)
    {
        movementSpeed *= multiplier;
        yield return new WaitForSeconds(duration);
        movementSpeed /= multiplier;
    }

}

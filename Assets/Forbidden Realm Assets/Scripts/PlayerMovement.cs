using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;
    public float moveInput;
    public float horizontalMove = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Horizontal Input", Mathf.Abs(moveInput));

        if (moveInput > 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput < 0f)
        {
            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveInput * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }
}
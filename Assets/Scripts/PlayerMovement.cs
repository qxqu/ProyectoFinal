using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 6f;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded = true;
    private Vector2 moveInput;

    [HideInInspector] public bool isShooting = false;
    [HideInInspector] public float shootTimer = 0f;
    private float shootCooldown = 0.25f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isShooting)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                isShooting = false;
                anim.SetBool("Shoot", false);
            }
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        bool isMoving = Mathf.Abs(moveInput.x) > 0.1f;

        bool isPressingShift = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isPressingShift ? runSpeed : walkSpeed;

        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jumping");
        }

        if (!isShooting)
        {
            anim.SetBool("isRunning", isMoving);
        }

        if (moveInput.x != 0)
            sr.flipX = moveInput.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}

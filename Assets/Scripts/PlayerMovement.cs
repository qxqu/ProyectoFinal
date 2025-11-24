using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 3f;     // Velocidad normal (caminar)
    public float runSpeed = 6f;      // Velocidad al mantener Shift
    public float jumpForce = 6f;     // Fuerza del salto

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded = true;
    private bool isRunning = false;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- Movimiento horizontal ---
        moveInput.x = Input.GetAxisRaw("Horizontal");

        // --- Detectar si se está moviendo (A o D) ---
        bool isMoving = Mathf.Abs(moveInput.x) > 0.1f;

        // --- Detectar si está corriendo (Shift + movimiento) ---
        bool isPressingShift = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isPressingShift ? runSpeed : walkSpeed;

        // --- Aplicar movimiento ---
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);

        // --- Saltar ---
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jumping");
        }

        // --- Animación de caminar/correr ---
        if (anim != null)
        {
            // Usa tu parámetro del Animator
            anim.SetBool("isRunning", isMoving);
        }

        // --- Invertir sprite según dirección ---
        if (moveInput.x != 0)
        {
            sr.flipX = moveInput.x < 0;
        }
    }

    // --- Detectar colisión con el suelo ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

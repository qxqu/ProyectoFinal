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
        // ---------------------------------------------------
        // CONTROL DE DISPARO (lo pone el PlayerAimAndShoot)
        // ---------------------------------------------------
        if (isShooting)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                isShooting = false;
                anim.SetBool("Shoot", false);
            }
        }

        // ---------------------------------------------------
        // MOVIMIENTO — Left Stick (PS4)
        // ---------------------------------------------------
        // "Horizontal" ya funciona con joystick izquierdo
        moveInput.x = Input.GetAxis("Horizontal");
        bool isMoving = Mathf.Abs(moveInput.x) > 0.1f;

        // Run si mantienes L3 presionado (círculo del stick)
        bool isPressingShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton8);
        float currentSpeed = isPressingShift ? runSpeed : walkSpeed;

        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, rb.linearVelocity.y);

        // ---------------------------------------------------
        // SALTO — botón X del control PS4
        // ---------------------------------------------------
        // "Jump" debe estar configurado con JoystickButton1 (X)
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.JoystickButton1)) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jumping");
        }

        // ---------------------------------------------------
        // ANIMACIONES
        // ---------------------------------------------------
        if (!isShooting)
            anim.SetBool("isRunning", isMoving);

        // ---------------------------------------------------
        // FLIP SPRITE
        // ---------------------------------------------------
        if (moveInput.x != 0)
            sr.flipX = moveInput.x < 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}

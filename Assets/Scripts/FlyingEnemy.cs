using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float speed = 2f;
    public float followRange = 15f;

    private Animator animator;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Kinematic;

        // IMPORTANTE: evita que el collider frene al enemigo
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange)
        {
            // Movimiento preciso hasta tocar al jugador
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );

            animator?.SetBool("isFlying", true);

            // Voltear sprite
            if (player.position.x > transform.position.x)
                transform.localScale = originalScale;
            else
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            animator?.SetBool("isFlying", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();

            if (health != null)
                health.TakeDamage();

            // El enemigo desaparece al tocar
            Destroy(gameObject);
        }
    }
}

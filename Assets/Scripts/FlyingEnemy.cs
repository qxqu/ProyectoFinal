using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float speed = 2f;
    public float followRange = 15f;
    public float stopDistance = 0.1f; // Se acerca casi completamente

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
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Ahora SIEMPRE se acerca hasta casi tocar al jugador
        if (distance < followRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            if (distance > stopDistance)
            {
                transform.position += (Vector3)direction * speed * Time.deltaTime;
            }

            animator?.SetBool("isFlying", true);

            // Voltear dependiendo de la dirección
            if (direction.x > 0)
                transform.localScale = originalScale;
            else if (direction.x < 0)
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
            {
                health.TakeDamage(1); // AHORA SÍ LE QUITA UN CORAZÓN
            }

            Destroy(gameObject); // El enemigo desaparece al golpear
        }
    }
}

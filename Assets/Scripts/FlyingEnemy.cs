using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player;
    public float speed = 2f;
    public float followRange = 10f;
    public float stopDistance = 1.5f;

    private Animator animator;
    private Vector3 originalScale;

    void Start()
    {
        // Guardar escala original
        originalScale = transform.localScale;

        // Asegurar rigidbody correctamente configurado
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;                    // NO gira
        rb.bodyType = RigidbodyType2D.Kinematic;
     
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < followRange && distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            animator?.SetBool("isFlying", true);

            // Voltear solo la X manteniendo el tamaño original
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

    // ESTA PARTE sigue igual (para daño al player)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage();
        }
    }
}

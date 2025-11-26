using UnityEngine;
using System.Collections;

public class PlayerAimAndShoot : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private Vector3 originalScale;
    private Collider2D playerCollider;
    private Animator anim;

    private PlayerMovement movement; // <-- IMPORTANTE

    void Start()
    {
        originalScale = transform.localScale;

        playerCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();  // <-- CONEXIÓN
    }

    void Update()
    {
        FlipTowardsMouse();

        // Disparo con clic derecho
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    void FlipTowardsMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;

        if (direction.x >= 0.01f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (direction.x <= -0.01f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        // Activar animación
        anim.SetBool("Shoot", true);
        movement.isShooting = true;

        StartCoroutine(StopShootAnimation());

        // Instanciar bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Ignorar colisión con jugador
        Collider2D bulletCol = bullet.GetComponent<Collider2D>();
        if (bulletCol != null && playerCollider != null)
            Physics2D.IgnoreCollision(bulletCol, playerCollider);

        // Aplicar velocidad
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;
    }

    private IEnumerator StopShootAnimation()
    {
        yield return new WaitForSeconds(0.15f); // Ajusta al largo de tu animación
        anim.SetBool("Shoot", false);
        movement.isShooting = false;
    }
}

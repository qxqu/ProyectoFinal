using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 10f; 

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
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
        // Posición del mouse en el mundo
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Dirección hacia el mouse
        Vector3 direction = mousePos - transform.position;

        // VOLTEO del jugador sin rotarlo
        if (direction.x >= 0.01f)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (direction.x <= -0.01f)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

        // Rotar SOLO el firePoint para apuntar
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;
    }
}

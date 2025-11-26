using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;       // Prefab de la bala
    public Transform firePoint;           // Lugar donde sale la bala
    public float bulletSpeed = 10f;       // Velocidad de la bala
    public float fireRate = 0.25f;        // Tiempo entre cada disparo

    private float nextFireTime = 0f;

    void Update()
    {
        // Disparar con clic izquierdo o con la tecla Espacio
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Crear bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Agregar velocidad
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.right * bulletSpeed;
        }

        // Destruir bala a los 3 segundos
        Destroy(bullet, 3f);
    }
}

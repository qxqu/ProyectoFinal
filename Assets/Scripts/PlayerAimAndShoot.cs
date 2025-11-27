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

    private PlayerMovement movement;

    // --- Control ---
    private Vector2 aimInput;          // Entrada del right stick
    private bool usingController = false;

    void Start()
    {
        originalScale = transform.localScale;

        playerCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        ReadAimInput();

        // Si se usa el mouse, apunta al mouse
        // Si se usa el right stick, apunta con el stick
        if (usingController)
            AimWithStick();
        else
            AimWithMouse();

        // Disparo con clic derecho o R1 (Fire1)
        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // -----------------------------------
    //         SISTEMA DE APUNTADO
    // -----------------------------------

    void ReadAimInput()
    {
        float rx = Input.GetAxis("RightStickHorizontal");
        float ry = Input.GetAxis("RightStickVertical");

        aimInput = new Vector2(rx, ry);

        // Si el jugador mueve el stick, usar controller
        if (aimInput.sqrMagnitude > 0.25f)
            usingController = true;

        // Si mueve el mouse, vuelve al mouse
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            usingController = false;
    }

    void AimWithMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector3 direction = mousePos - transform.position;

        FlipPlayer(direction.x);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void AimWithStick()
    {
        if (aimInput.sqrMagnitude < 0.1f) return; // No mover si no hay input

        Vector3 direction = aimInput.normalized;

        FlipPlayer(direction.x);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void FlipPlayer(float xDir)
    {
        if (xDir >= 0.01f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (xDir <= -0.01f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    // -----------------------------------
    //               DISPARO
    // -----------------------------------

    void Shoot()
    {
        anim.SetBool("Shoot", true);
        movement.isShooting = true;

        StartCoroutine(StopShootAnimation());

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Collider2D bulletCol = bullet.GetComponent<Collider2D>();
        if (bulletCol != null && playerCollider != null)
            Physics2D.IgnoreCollision(bulletCol, playerCollider);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;
    }

    private IEnumerator StopShootAnimation()
    {
        yield return new WaitForSeconds(0.15f);
        anim.SetBool("Shoot", false);
        movement.isShooting = false;
    }
}

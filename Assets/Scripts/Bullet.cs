using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bala golpeó: " + collision.collider.name);

        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject); // Mata enemigo
            Destroy(gameObject); // Destruye bala
        }
    }
}

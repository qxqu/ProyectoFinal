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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bala golpeó: " + collision.name);

        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); 

            
            if (RoundManager.Instance != null)
            {
                RoundManager.Instance.OnEnemyKilled();
            }
        }

        Destroy(gameObject); 
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Lives")]
    public int maxLives = 3;
    private int currentLives;

    [Header("UI Hearts")]
    public Image[] heartImages;       // Asigna los corazones del Canvas
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private HeartNode head;           // Lista enlazada de corazones

    [Header("Invincibility")]
    public float invincibleTime = 1f;
    private bool isInvincible = false;

    private void Start()
    {
        currentLives = maxLives;
        BuildLinkedList();
        UpdateHearts();
    }

    // Construye la lista enlazada basada en el array del inspector
    private void BuildLinkedList()
    {
        HeartNode prev = null;

        for (int i = 0; i < heartImages.Length; i++)
        {
            HeartNode newNode = new HeartNode(heartImages[i]);

            if (head == null)
                head = newNode;
            else
                prev.next = newNode;

            prev = newNode;
        }
    }

    public void TakeDamage()
    {
        if (isInvincible)
            return;

        currentLives--;

        if (currentLives <= 0)
        {
            currentLives = 0;
            UpdateHearts();
            Die();
            return;
        }

        UpdateHearts();
        StartCoroutine(Invincibility());
    }

    private IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void Die()
    {
        Debug.Log("El jugador murió");

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.ShowGameOver();
        else
            Debug.LogError("NO HAY GameOverManager en la escena.");
    }

    private void UpdateHearts()
    {
        HeartNode current = head;
        int index = 0;

        while (current != null)
        {
            if (index < currentLives)
                current.heartImage.sprite = fullHeart;
            else
                current.heartImage.sprite = emptyHeart;

            current = current.next;
            index++;
        }
    }
}

// Nodo de lista enlazada para manejar corazones
public class HeartNode
{
    public Image heartImage;
    public HeartNode next;

    public HeartNode(Image img)
    {
        heartImage = img;
        next = null;
    }
}

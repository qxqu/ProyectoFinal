using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public Image[] heartImages;   // Arrastras los corazones desde el Canvas
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private HeartNode head;       // Inicio de la lista enlazada

    private bool isInvincible = false;
    public float invincibleTime = 1f;

    void Start()
    {
        currentLives = maxLives;
        BuildLinkedList();
        UpdateHearts();
    }

    // Construye la lista enlazada usando el array del inspector
    void BuildLinkedList()
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
            Debug.Log("El jugador murió");
            // Aquí puedes reiniciar la escena o abrir menú Game Over
        }

        UpdateHearts();
        StartCoroutine(Invincibility());
    }

    void UpdateHearts()
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

    private System.Collections.IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }
}

// Nodo de lista enlazada
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

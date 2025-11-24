using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public Sprite[] skinSprites;         // Asignas tus 3 skins
    public SpriteRenderer playerSprite;  // SpriteRenderer del personaje

    private SkinNode head;
    private SkinNode current;

    void Start()
    {
        BuildCircularList();

        // Cargamos la skin seleccionada anteriormente
        int index = SkinSelector.savedSkinIndex;

        current = GetSkinNode(index);

        ApplySkin();
    }

    void BuildCircularList()
    {
        SkinNode prev = null;

        for (int i = 0; i < skinSprites.Length; i++)
        {
            SkinNode newNode = new SkinNode(skinSprites[i]);

            if (head == null)
                head = newNode;
            else
                prev.next = newNode;

            prev = newNode;

            if (i == skinSprites.Length - 1)
                newNode.next = head; // circular
        }
    }

    SkinNode GetSkinNode(int index)
    {
        SkinNode temp = head;

        for (int i = 0; i < index; i++)
        {
            temp = temp.next;
        }

        return temp;
    }

    void ApplySkin()
    {
        playerSprite.sprite = current.skin;
    }
}

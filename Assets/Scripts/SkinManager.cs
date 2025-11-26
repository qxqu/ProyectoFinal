using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public RuntimeAnimatorController[] skinControllers; 
    
    public Animator playerAnimator; 

    private SkinNode head;
    private SkinNode current;

    void Start()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }

        BuildCircularList();

        int savedIndex = PlayerPrefs.GetInt("SelectedSkin", 0);
        current = GetSkinNode(savedIndex);
        ApplySkin(); 
    }

    void BuildCircularList()
    {
        SkinNode prev = null;

        for (int i = 0; i < skinControllers.Length; i++)
        {
            SkinNode newNode = new SkinNode(skinControllers[i]);

            if (head == null) head = newNode;
            else prev.next = newNode;

            prev = newNode;

            if (i == skinControllers.Length - 1)
                newNode.next = head;
        }
    }

    SkinNode GetSkinNode(int index)
    {
        if (head == null) return null;
        SkinNode temp = head;
        for (int i = 0; i < index; i++)
        {
            temp = temp.next;
        }
        return temp;
    }

    void ApplySkin()
    {
        if (current != null && current.controller != null)
        {
            playerAnimator.runtimeAnimatorController = current.controller;
        }
    }
}
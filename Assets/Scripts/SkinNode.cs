using UnityEngine;

public class SkinNode
{
    public RuntimeAnimatorController controller; 
    public SkinNode next;

    public SkinNode(RuntimeAnimatorController c)
    {
        controller = c;
        next = null;
    }
}
using UnityEngine;

public class SkinNode
{
    public Sprite skin;
    public SkinNode next;

    public SkinNode(Sprite s)
    {
        skin = s;
        next = null;
    }
}

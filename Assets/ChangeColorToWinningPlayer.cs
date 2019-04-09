using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorToWinningPlayer : MonoBehaviour
{
    SpriteRenderer[] spriteRenderersInChildren;

    public Color defaultColor;

    private void Awake()
    {
        spriteRenderersInChildren = GetComponentsInChildren<SpriteRenderer>();
    }

    public void setColor( Color newColor)
    {
        foreach (SpriteRenderer sprite in spriteRenderersInChildren)
        {
            sprite.color = newColor;
        }
    }
}

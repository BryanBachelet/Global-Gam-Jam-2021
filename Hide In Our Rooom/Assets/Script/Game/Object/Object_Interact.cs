using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Interact : MonoBehaviour
{
    public Color classicColor = Color.white;
    public Color colorInteract = Color.blue;
    public bool isInteract = false;
    public int resetFrame = 3;

    private int countFrame = 1;
    private SpriteRenderer spriteRenderer;

    public void ActiveInteract()
    {
        isInteract = true;
        countFrame = 0;
    }

    public virtual void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        if (isInteract)
        {
            spriteRenderer.color = colorInteract;
        }
        else
        {
            spriteRenderer.color = classicColor;
        }

    }

    public virtual void LateUpdate()
    {
        countFrame++;
        if (countFrame > resetFrame)
        {
            isInteract = false;
        }
    }
}

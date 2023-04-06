using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : Singleton<Hover>
{
    private SpriteRenderer spriteTarget;
    void Start()
    {
        spriteTarget = GetComponent<SpriteRenderer>();
        spriteTarget.enabled = false;
    }

 
    void Update()
    {
        if (spriteTarget.enabled)
        {
            FolowMouse();
        }
        
    }

    private void FolowMouse()
    {
        Vector2 targetpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(targetpos.x,targetpos.y,0);
    }
    public void Activate(Sprite sprite)
    {
        spriteTarget.sprite = sprite;
        spriteTarget.enabled = true;
    }
    public void Disactivate()
    {
        spriteTarget.enabled = false;
        GameManager.Instance.ClickedButton = null;
    }
}

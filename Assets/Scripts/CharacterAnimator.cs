using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Sprite> spritesheet;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void AnimateWalk(Vector2 direction) {
        /*
        spritesheet arrangement:
         0 | 1 | 2
        --- --- ---
         3 | 4 | 5 
        --- --- ---
         6 | 7 | 8
        */
        int sprite = (int) (Mathf.Round(direction.x + 1) + Mathf.Round(-direction.y + 1)*3);
        sprite = sprite == 4 ? 5 : sprite;
        spriteRenderer.sprite = spritesheet[sprite];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (delta.magnitude > 0.01f) {
            AnimateWalk(delta.normalized);
        }
    }
}

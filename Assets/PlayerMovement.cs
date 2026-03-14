using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = direction.normalized * Speed;

        if (direction.x != 0)
        {
            ResetLayers();
            anim.SetLayerWeight(2, 1);

            if (direction.x > 0)
            {
                sprite.flipX = true;
            }
            else if (direction.x < 0)
            {
                sprite.flipX = false;
            }
        }

        if (direction.y > 0 && direction.x == 0)
        {
            ResetLayers();
            anim.SetLayerWeight(1, 1);
        }

        if (direction.y < 0 && direction.x == 0)
        {
            ResetLayers();
            anim.SetLayerWeight(0, 1);
        }

        if (direction != Vector2.zero)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false );
        }
    }

    private void ResetLayers()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
    }
}
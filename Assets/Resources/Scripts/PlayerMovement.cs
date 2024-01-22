using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    public float moveSpeed;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput == Vector2.zero)
        {
            animator.SetBool("isRunning", false);
        }
        else if (PlayerPrefs.GetInt("isFrozen") != 1)
        {
            animator.SetBool("isRunning", true);
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            }
            if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
        }
    }

    private void OnFire()
    {
        if (PlayerPrefs.GetInt("isFrozen") != 1)
        {
            animator.SetTrigger("attacking");
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        int isFrozen = PlayerPrefs.GetInt("isFrozen");
        if (isFrozen != 1) 
        {
            rb.AddForce(moveInput * moveSpeed);
        }
    }

    void OnDamage()
    {
        animator.SetTrigger("isDamaged");
    }

    void OnDie()
    {
        animator.SetTrigger("isDead");
        PlayerPrefs.SetInt("PlayerStatus", 0);
    }
}
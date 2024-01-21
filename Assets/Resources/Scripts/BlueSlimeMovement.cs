using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlimeMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    Animator animator;
    SpriteRenderer spriteRenderer;
    DetectionZone zone;
    Vector2 direction;
    private int attackPower;
    public int knockbackForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        zone = GetComponent<DetectionZone>();
    }

    public void OnJump(Vector2 direction)
    {
        animator.SetBool("isJumping", true);
    }

    public void Move()
    {
        rb.AddForce(direction.normalized * moveSpeed);
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public  void onLand()
    {
        animator.SetBool("isJumping", false);
        rb.velocity = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if(zone.detectedObjs != null)
        {
            direction = (zone.detectedObjs.transform.position - transform.position);
            if (direction.magnitude <= zone.viewRadius)
            {
                OnJump(direction);
            } else
            {
                onLand();
            }
        }
    }   

    void OnDamage() 
    {
        animator.SetTrigger("isDamaged");
    }

    void OnDie()
    {
        animator.SetTrigger("isDead");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null && collider.tag == "Player")
        {
            Vector2 direction = collider.transform.position - transform.position;

            attackPower = 1;
            damageable.OnHit(attackPower, direction.normalized * knockbackForce);
        }
    }
}

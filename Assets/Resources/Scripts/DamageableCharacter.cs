using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;
    Collider2D physicsCollider;

    public float health;
    public float maxHealth;

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                gameObject.BroadcastMessage("OnDie");
                gameObject.BroadcastMessage("BarFiller", health / maxHealth);
                Targetable = false;
            }
            else
            {
                gameObject.BroadcastMessage("OnDamage");
                gameObject.BroadcastMessage("BarFiller",health / maxHealth);
            }
        }
    }

    bool targetable;

    public bool Targetable
    {
        get
        {
            return targetable;
        }
        set
        {
            targetable = value;
            if (!targetable)
            {
                rb.simulated = false;
            }
        }
    }
    public void OnHit(int damage, Vector2 kb)
    {
        Health -= damage;
        rb.AddForce(kb);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = rb.GetComponent<Collider2D>();
    }

    public void onObjectDestroyed()
    {
        Destroy(gameObject);
    }
}

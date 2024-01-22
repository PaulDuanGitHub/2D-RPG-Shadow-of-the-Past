using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    Animator animator;
    SpriteRenderer spriteRenderer;
    DetectionZone zone;
    Vector2 direction;
    private int attackPower;
    public int knockbackForce;

    private bool isSkillActive = false;
    private float skillCooldown = 5f; // fire cooldown
    /*
     * skill: randomly wait for 0-5s -> active skills -> 5s cooldown(include animation) -> head
     */

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
        StartCoroutine(ActivateSkillRandomly());
    }
    IEnumerator ActivateSkillRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0f, 5f)); // wait for 0-5s

            if (!isSkillActive)
            {
                ActivateSkill();
            }
        }
    }

    void ActivateSkill()
    {
        // active fire
        //Debug.Log("触发敌人技能!");
        animator.SetTrigger("attacking");

        // start cooldown
        isSkillActive = true;
        StartCoroutine(SkillCooldown());
    }

    IEnumerator SkillCooldown()
    {
        yield return new WaitForSeconds(skillCooldown);

        // cooldown finished
        isSkillActive = false;
    }

    public void OnRunning()
    {
        animator.SetBool("isRunning", true);
    }

    public  void onStop()
    {
        animator.SetBool("isRunning", false);
        rb.velocity = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if(zone.detectedObjs != null)
        {
            direction = (zone.detectedObjs.transform.position - transform.position);
            if (direction.magnitude <= zone.viewRadius)
            {
                OnRunning();
                rb.AddForce(direction.normalized * moveSpeed);
                if (direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                    gameObject.BroadcastMessage("IsFacingRight", true);
                }
                if (direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                    gameObject.BroadcastMessage("IsFacingRight", false);
                }
            } else
            {
                onStop();
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
        PlayerPrefs.SetInt("KingStatus", 0);
    }
}


using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class enemyScript : MonoBehaviour, IDamageable
{
    public int health = 100;
    private Vector3 player;
    public float moveSpeed = 5f;
    bool isTouchingPlayer;
    public int damage;
    int initialDamage;
    playerMovement playerMovement;

    Vector3 offset;
    public float offset1;
    public Rigidbody2D rb;
    public FogManager fogManager;

    public float dmgcooldown;

    public float initialSpeed;
    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerMovement = playerObject.GetComponent<playerMovement>();

        offset = Random.insideUnitCircle * offset1;
        rb = transform.GetComponent<Rigidbody2D>();


        fogManager = GameObject.FindObjectOfType<FogManager>();


        initialSpeed = moveSpeed;
        initialDamage = damage;

        StartCoroutine(DamageTimer());
    }
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.position;




        if (!fogManager.doorsLocked)
        {
            moveSpeed = 0f;
        }
        else
        {
            moveSpeed = initialSpeed;
        }


        
    }

    private void FixedUpdate() {
        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform.position;
        //following 
        bool isFollowing = false;
        float distance = Vector3.Distance(transform.position, player);
        if (distance <= 8f) isFollowing = true;
        else if (distance >= 20f) isFollowing = false;
        
        if (isFollowing) MoveToPlayer();
    }

    IEnumerator DamageTimer()
    {
        while (true)
        {
            damage = initialDamage;

            if (isTouchingPlayer && damage > 0)
            {
                if (dmgcooldown == 0)
                {
                    playerMovement.TakeDamage1(damage);
                    break;
                }
                playerMovement.TakeDamage1(damage);

            }


            damage = 0;
            yield return new WaitForSeconds(dmgcooldown);
        
        }
    }

   
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isTouchingPlayer = true;   
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isTouchingPlayer = false;  
            
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    
    void MoveToPlayer()
    {
       
        if (player != null)
        {
            Vector3 target = player + offset;
            rb.MovePosition(Vector3.MoveTowards(transform.position, target, moveSpeed * Time.fixedDeltaTime));
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
    
}

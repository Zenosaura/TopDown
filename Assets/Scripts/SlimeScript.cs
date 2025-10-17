using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;


public class SlimeScript : MonoBehaviour, IDamageable
{
    public float health = 100f;

    private Rigidbody2D rb;
    private Vector3 player;
    public float moveSpeed = 5f;
    bool isTouchingPlayer;
   
    public int damage;
    int initialDamage;
    playerMovement playerMovement;

    float tempHealth;
    public GameObject slimePrefab;

    Vector3 offset;
    public float offset1 = 1f;

    public float initialSpeed;
    public FogManager fogManager;

    public float dmgcooldown;
    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        rb = transform.GetComponent<Rigidbody2D>();
        playerMovement = playerObject.GetComponent<playerMovement>();

        offset = Random.insideUnitCircle * offset1;
        tempHealth = health;

        fogManager = GameObject.FindObjectOfType<FogManager>();

        initialSpeed = moveSpeed;

        initialDamage = damage;

        StartCoroutine(DamageTimer());

    }
    void Update()
    {


        
        
        
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
        if (distance <= 6f) isFollowing = true;
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
            SpawnSlime();
            Destroy(gameObject);
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
    public int generation = 0;
    public int maxGen = 2;

    void SpawnSlime()
    {

        if (generation < maxGen)
        {
            for (int i = 0; i < 2; i++)
            {
                float newHealth = tempHealth / 2;
                int newDamage = damage / 2;
                float newSpeed = moveSpeed * 1.02f;

                GameObject newSlime = Instantiate(slimePrefab, transform.position, Quaternion.identity);


                SlimeScript slimePrefabScript = newSlime.GetComponent<SlimeScript>();
                slimePrefabScript.health = newHealth;
                slimePrefabScript.damage = newDamage;
                slimePrefabScript.moveSpeed = newSpeed;
                newSlime.transform.localScale *= 0.75f;
                slimePrefabScript.generation++;
                
            
            }
            
        }
    }
}

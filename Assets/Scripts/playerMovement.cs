using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerMovement : MonoBehaviour
{
    public int moveSpeed = 5;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    public TextMeshProUGUI healthText;
    public SoundScript soundScript;
    public int maxHealth;
    public int initialSpeed;
    // Update is called once per frame
    void Awake()
    {
        maxHealth = health;
        initialSpeed = moveSpeed;
    }
    void Update()
    {
        if (health <= 0)
        {
            healthText.text = "RIP";
        }
        else
        {
            healthText.text = ""+ health;
        }
        if (isAlive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    // HEALTH
    bool isAlive = true;
    public int health = 100;
    public void TakeDamage1(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            soundScript.HurtSound();
        }
        else
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("YOU DIED");
        isAlive = false;
    }
}

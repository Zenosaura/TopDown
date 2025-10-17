using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    playerMovement playerMovement;
    SoundScript soundScript;
    public int healAmount;

    public string potionName = "Healed!";
    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerMovement = playerObject.GetComponent<playerMovement>();
        soundScript = GameObject.FindAnyObjectByType<SoundScript>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundScript.HealSound();
            if (playerMovement.health == playerMovement.maxHealth) Debug.Log("MAX");
            else playerMovement.health += healAmount;
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    playerMovement playerMovement;
    SoundScript soundScript;
    public int speedAmount;
    public static int speedTime = 10;
    Collider2D collider2;
     public static bool speedUp = false; 
   
    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerMovement = playerObject.GetComponent<playerMovement>();
        soundScript = GameObject.FindAnyObjectByType<SoundScript>();
        collider2 = GetComponent<Collider2D>();

    }
    bool started = false;

    IEnumerator SpeedUp()
    {

        soundScript.SpeedSound();
        SpeedPotion.speedUp = true;
        playerMovement.moveSpeed += speedAmount;

        yield return new WaitForSeconds(speedTime);
        SpeedPotion.speedUp = false;
        playerMovement.moveSpeed = playerMovement.initialSpeed;
        Destroy(gameObject, 0.1f);
        
        
    }
    
    bool isTouchingPlayer = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !started)
        {
            isTouchingPlayer = true;

            StartCoroutine(SpeedUp());
            Debug.Log("Speed");
            started = true;
            GetComponent<SpriteRenderer>().enabled = false; // hides the sprite
            collider2.enabled = false; // disables interaction

        
            
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPotion : MonoBehaviour
{

    SoundScript soundScript;
    public static bool dmgUp = false;
    public static float DmgAmount = 1.5f;

    
    public static float dmgTime = 10;
    Collider2D collider2;
    void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        collider2 = GetComponent<Collider2D>();
        soundScript = GameObject.FindAnyObjectByType<SoundScript>();
    }
    bool started = false;

    bool isTouchingPlayer = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !started)
        {
            isTouchingPlayer = true;

            StartCoroutine(DmgUpI());
            Debug.Log("DMG");
            started = true;
            GetComponent<SpriteRenderer>().enabled = false; // hides the sprite
            collider2.enabled = false; // disables interaction



        }

    }


    IEnumerator DmgUpI()
    {
        soundScript.SpeedSound();
        DmgPotion.dmgUp = true;

        yield return new WaitForSeconds(dmgTime);

        DmgPotion.dmgUp = false;
        Destroy(gameObject);
    }


}

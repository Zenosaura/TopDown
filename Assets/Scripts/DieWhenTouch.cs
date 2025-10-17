using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieWhenTouch : MonoBehaviour
{
    GameObject player;
    bool touched = false;
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            touched = true;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds((float)0.05);
        Destroy(gameObject);
    }
    void Update()
    {
        if (touched == true)
        {
            StartCoroutine(Wait());
            
        } 
    }

}


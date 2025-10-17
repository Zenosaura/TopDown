using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rocketImpact : MonoBehaviour
{
    public bulletbIG bulletBig;
    bool impacted = false;
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (impacted) return;
        impacted = true;
        enemyScript enemy = hitInfo.GetComponent<enemyScript>();
        if (enemy != null)
        {
            enemy.TakeDamage(bulletBig.dmg);
            Debug.Log(enemy.health);
        }
    }
}

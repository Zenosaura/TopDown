
using System.Collections.Generic;

using UnityEngine;

public class roomZone : MonoBehaviour
{
    public List<GameObject> enemiesInRoom = new List<GameObject>();

    public List<GameObject> totemObject = new List<GameObject>();
    public List<GameObject> notTotem = new List<GameObject>();
    
    public List<GameObject> player = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies"))
        {
            enemiesInRoom.Add(collision.gameObject);

            if (collision.GetComponent<TotemAffected>() != null)
            {
                totemObject.Add(collision.gameObject);
            }
            else
            {
                notTotem.Add(collision.gameObject);
            }
        }
        if (collision.CompareTag("Player"))
        {
            player.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies"))
        {
            enemiesInRoom.Remove(collision.gameObject);

            totemObject.Remove(collision.gameObject);
            notTotem.Remove(collision.gameObject);
        }
    }

    private float clearTimer = 0f;
    public float clearDelay = 0.5f; // half a second buffer

    public bool RoomCleared()
    {
        enemiesInRoom.RemoveAll(e => e == null);

        if (enemiesInRoom.Count == 0)
        {
            clearTimer += Time.deltaTime;
            if (clearTimer >= clearDelay)
            {
                return true;
            }
        }
        else
        {
            clearTimer = 0f; // reset if enemies are present
        }

        return false;
    
    }
}

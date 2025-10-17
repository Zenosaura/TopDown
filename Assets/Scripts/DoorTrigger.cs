using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool walkedThrough = false;
    public roomZone roomZone;
    public roomZone activeZone = null;

    void Awake()
    {
        roomZone = GetComponentInChildren<roomZone>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !walkedThrough)
        {
            walkedThrough = true;
            activeZone = roomZone;
            activeZone.enabled = true;
            Debug.Log($"Activated room: {activeZone.name}");
        }
    }
}

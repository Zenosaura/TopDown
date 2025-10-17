using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogManager : MonoBehaviour
{
    public List<roomZone> roomZoness;
    public TilemapRenderer tilemapRenderer;
    public TilemapCollider2D collider1;

    public bool doorsLocked = false;
    private roomZone currentRoom = null;

    void Start()
    {
        roomZoness = new List<roomZone>(FindObjectsOfType<roomZone>());

        // Initialize door state: invisible and walkable
        tilemapRenderer.enabled = false;
        collider1.isTrigger = true;
    }

    void Update()
    {
        foreach (DoorTrigger dt in FindObjectsOfType<DoorTrigger>())
        {
            roomZone zone = dt.activeZone;

            if (zone != null && dt.walkedThrough)
            {
                if (currentRoom != zone)
                {
                    currentRoom = zone;
                    LockAllDoors();
                    doorsLocked = true;
                    Debug.Log($"Entered new room: {zone.name} — doors locked");
                }

                if (zone.RoomCleared())
                {
                    UnlockAllDoors();
                    dt.walkedThrough = false;
                    dt.activeZone = null;
                    doorsLocked = false;
                    currentRoom = null;
                    Debug.Log($"Room cleared: {zone.name} — doors unlocked");
                }
            }
        }
    }
 
    void LockAllDoors()
    {
        tilemapRenderer.enabled = true;
        collider1.isTrigger = false;

        // Force collider refresh
        collider1.enabled = false;
        collider1.enabled = true;

       
        
    }

    void UnlockAllDoors()
    {
        tilemapRenderer.enabled = false;
        collider1.isTrigger = true;

        // Force collider refresh
        collider1.enabled = false;
        collider1.enabled = true;

        // Reset walkedThrough on all DoorTriggers
        foreach (DoorTrigger dt in FindObjectsOfType<DoorTrigger>())
        {
            dt.walkedThrough = false;
        }


        Debug.Log("UNLOCKED");
    }
}

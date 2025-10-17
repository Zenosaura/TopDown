using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PickupManager : MonoBehaviour
{
    public weaponManager weaponManager;
    public int index;
    Vector3 playerPos;
    bool isInPlayerRange = false;
    TextMeshProUGUI weaponName;

    

    
    public string internalName;
    
    void Awake()
{
    weaponName = GameObject.FindWithTag("WeaponName").GetComponent<TextMeshProUGUI>();
    // Auto-assign weaponManager if it's not set in the Inspector
    if (weaponManager == null)
    {
        GameObject player = GameObject.FindWithTag("Player");
       
        if (player != null)
            {
                weaponManager = player.GetComponent<weaponManager>();
                if (weaponManager == null)
                    Debug.LogWarning("Player found, but no weaponManager script attached!");
            }
            else
            {
                Debug.LogWarning("No GameObject with 'Player' tag found!");
            }
    }
}

    void DisplayWeaponName(int index)
    {
        weaponName.text = internalName;
        
    }
    void Update()
    {
        
        playerPos = GameObject.FindWithTag("Player").transform.position;
        
        if (isInPlayerRange && Input.GetKeyDown(KeyCode.E))
        {
            if (weaponManager.hasWeapon)
            {
                int previousIndex = weaponManager.currentIndex;
                
                Vector3 dropPos = playerPos + new Vector3(0f, 1f, 0f);

                Quaternion dropRotation  = weaponManager.weaponPickupPrefabs[previousIndex].transform.rotation;

                GameObject droppedWeapon = Instantiate(weaponManager.weaponPickupPrefabs[previousIndex], dropPos, dropRotation);

                PickupManager dropScript = droppedWeapon.GetComponent<PickupManager>();
                if (dropScript != null)
                {
                    dropScript.index = previousIndex; // 🔑 Pass correct index
                    dropScript.weaponManager = weaponManager; // 🔌 Reconnect the reference
                }
            }
            
            weaponManager.SelectWeapon(index);
            
            weaponManager.DisablePickupPopup();
            Destroy(gameObject);
            
        }
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInPlayerRange = true;
            weaponManager.EnablePickupPopup();
            DisplayWeaponName(index);

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInPlayerRange = false;
            weaponManager.DisablePickupPopup();

        }
    }
}

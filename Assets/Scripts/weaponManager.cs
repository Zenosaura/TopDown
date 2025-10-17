
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class weaponManager : MonoBehaviour
{
    public List<GameObject> weaponSlots = new List<GameObject>();
    public Transform weaponHolder;
    public bool hasWeapon = false;
    public int currentIndex = -1;
    public SoundScript soundScript;
    public GameObject[] weaponPickupPrefabs;


    [SerializeField] Image pickupPopup;
    
    Vector3 playerPos;
    


    void Awake()
    {
        

        if (weaponHolder == null)
        {
            Transform found = transform.Find("Aim");
            if (found != null)
                weaponHolder = found;
            else
                Debug.LogWarning("Weapon Holder (Aim) not found in Player prefab!");
        }

        // Auto-assign weaponSlots from WeaponSlots container
        Transform slotsContainer = weaponHolder;
        if (slotsContainer != null)
        {
            weaponSlots.Clear();
            foreach (Transform slot in slotsContainer)
            {
                slot.gameObject.SetActive(false); // disable at start
                weaponSlots.Add(slot.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("No 'WeaponSlots' container found under Player!");
        }



    }

    void Start()
    {
        DisablePickupPopup();
    }
    void Update()
    {
        


        playerPos = GameObject.FindWithTag("Player").transform.position;
        //because the is a canvas, so has to use screenpos
        Vector3 screenPos = Camera.main.WorldToScreenPoint(playerPos + new Vector3(0, 0.75f, 0));

        pickupPopup.transform.position = screenPos;

        
    }
    public void SelectWeapon(int index)
    {
        EnablePickupPopup();
        currentIndex = index;
        hasWeapon = true;
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            
            weaponSlots[i].SetActive(i == index);
            if (weaponPickupPrefabs[index].CompareTag("Swords") == true)
            {
                soundScript.pickupSwordSound();
            }
            else
            {
                soundScript.pickupGunSound();
            }
            
        }
    }

    public void EnablePickupPopup()
    {
        
        pickupPopup.gameObject.SetActive(true);
    }


    public void DisablePickupPopup()
    {
        pickupPopup.gameObject.SetActive(false);
    }
}

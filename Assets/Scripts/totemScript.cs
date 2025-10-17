using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class totemScript : MonoBehaviour
{

    public TextMeshProUGUI totemText;
    public static GameObject totemEnemy;

    // Update is called once per frame
    void Update()
    {
        roomZone[] allZones = FindObjectsOfType<roomZone>();

        foreach (roomZone zone in allZones)
        {
            zone.totemObject.RemoveAll(e => e == null);
            zone.notTotem.RemoveAll(e => e == null);

            bool totemAlive = zone.totemObject.Count > 0;

            foreach (GameObject enemyObj in zone.notTotem)
            {
                if (totemAlive)
                {
                    
                    if (zone.player.Count > 0)
                    {
                        totemText.enabled = true;
                    }
                    enemyObj.tag = "Untagged";
                    enemyObj.layer = LayerMask.NameToLayer("Default");
                }
                else
                {
                    

                    totemText.enabled = false;
                    enemyObj.tag = "Enemies";
                    enemyObj.layer = LayerMask.NameToLayer("Enemy");
                }
            }


        }









    }

}
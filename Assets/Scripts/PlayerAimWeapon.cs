using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlayerAimWeapon : MonoBehaviour
{
    

    public Transform aimTransform;
    void Update()
    {
        Aiming();
        Shooting();
    }
    void Aiming()
    {

        Vector3 mousePos = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePos - transform.position).normalized;

        //FLIP gun
        if (aimDirection.x < 0)
        {
            aimTransform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            aimTransform.localScale = new Vector3(1, 1, 1);
        }

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        
    }
    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
 
            
        }

        

    }




}

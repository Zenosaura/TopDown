using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{

    public int ammo;
    public int MaxAmmo;
    public Sprite internalGunSprite;
    public TextMeshProUGUI ammoText;
    public Image gunSprite;
    int tempAmmo;
    bool isReloading = false;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public SoundScript soundScript;
    public Animator animator;

    public float fireRate = 2f;
    float nextAttackTime = 0f;
    void Awake()
    {
        tempAmmo = ammo;
        MaxAmmo = ammo;
    }
    void OnEnable()
    {
        ammoText.text = ammo + "/" + MaxAmmo;
        gunSprite.sprite = internalGunSprite;
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextAttackTime)
        {
            if ((Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) && canShoot && !isReloading)
            {
                StartCoroutine(Shoot());
                if (ATKPotion.dmgUp == true) nextAttackTime = Time.time + 1f / (fireRate * ATKPotion.DmgAmount);
                else nextAttackTime = Time.time + 1f / fireRate;
            }
        }

        if ((Input.GetKeyDown(KeyCode.R) || ammo == 0) && !isReloading)
        {
            // dont get mouse button
            StartCoroutine(Reload());
        }
    }
    bool canShoot = true;

    IEnumerator Shoot()
    {
        if (!canShoot) yield break;
        canShoot = false;

        soundScript.shootSound();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        animator.SetTrigger("CanShoot");
        ammo--;
        ammoText.text = ammo + "/" + tempAmmo;
        

        if (ammo > 0) canShoot = true;

    }

    IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "Reloading...";
        soundScript.ReloadSound();
        yield return new WaitForSeconds((float)1.5);
        ammo = tempAmmo;
        ammoText.text = ammo + "/" + tempAmmo;

        isReloading = false;
        canShoot = true;
    }
}

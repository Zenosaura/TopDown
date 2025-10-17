
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MeleeWeapon : MonoBehaviour
{

    public int dmg = 25;
    public Sprite internalGunSprite;
    public TextMeshProUGUI WeaponText;
    public Image weaponSprite;

    public Transform attackPoint;
    public float attackRangeS;
    public Vector2 boxSize = new Vector2(1f, 2f);
    public GameObject slash;
    public LayerMask enemyLayers;

    public string internalWeapName;
    public SoundScript soundScript;

    public float fireRate = 2f;
    float nextAttackTime = 0f;


    public TextMeshProUGUI dmgNum;
    Canvas uiCanvas;

    public Animator animator;

    public GameObject impactEffect;
    public Transform swordEnd;
    void Awake()
    {
        if (uiCanvas == null)
        {
            uiCanvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        }
    }
    void OnEnable()
    {
        WeaponText.text = internalWeapName;
        weaponSprite.sprite = internalGunSprite;

        
    
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                if (ATKPotion.dmgUp == true) nextAttackTime = Time.time + 1f / (fireRate * ATKPotion.DmgAmount);
                else nextAttackTime = Time.time + 1f / fireRate;
            }
        }


    }


    void Attack()
    {
        StartCoroutine(slash_visible());
        soundScript.slashSound();

        // Get rotation angle in degrees
        Vector2 direction = attackPoint.right.normalized;
        // Get angle from direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Collider2D[] hitEnemies = null;

        //detect enemies in range of attack

        if (gameObject.name == "Sword")
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRangeS, enemyLayers);
        }
        else if (gameObject.name == "Rapier")
        {
            hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, boxSize, angle , enemyLayers); 
        }


        //damage 
        foreach (Collider2D enemy in hitEnemies)
        {
            //damage
            int finalDmg;
            if (DmgPotion.dmgUp) finalDmg = (int)(dmg * DmgPotion.DmgAmount);
            else finalDmg = dmg;
            ShowDmgText(finalDmg);
            IDamageable target = enemy.GetComponent<IDamageable>();
            
            if (target != null)
            {
                if(DmgPotion.dmgUp == true) target.TakeDamage((int)(dmg * DmgPotion.DmgAmount));
                else target.TakeDamage(dmg);
            }


            
            ShowImpactEffect();
        }


    }

    IEnumerator slash_visible()
    {
        animator.SetTrigger("Slashing");
        
        yield return null;
        
    }
    
    void ShowImpactEffect()
    {
        Instantiate(impactEffect, swordEnd.transform.position, Quaternion.identity);
        
    }
    void ShowDmgText(int dmg)
    {
        // Convert world position to canvas position
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.transform as RectTransform,
            Camera.main.WorldToScreenPoint(transform.position - new Vector3(-2f, 0,0)),
            null,
            out canvasPos
        );

        TextMeshProUGUI dmgTextInstance = Instantiate(dmgNum, uiCanvas.transform);
        dmgTextInstance.text = dmg.ToString();
        dmgTextInstance.color = Random.ColorHSV(0f, 0.1f, 0.5f, 1f, 0.8f, 1f);

        // Set anchored position
        RectTransform rectTransform = dmgTextInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = canvasPos;
        
    }




    //see attack range
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        if (gameObject.name == "Sword") Gizmos.DrawWireSphere(attackPoint.position, attackRangeS);
        else if (gameObject.name == "Rapier")
        {
            Vector2 direction = attackPoint.right.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Gizmos.color = Color.white;
            Gizmos.matrix = Matrix4x4.TRS(attackPoint.position, Quaternion.Euler(0, 0, angle), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, boxSize);
    
        }

    }
}

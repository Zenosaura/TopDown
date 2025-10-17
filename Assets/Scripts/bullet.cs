using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public int dmg;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    public TextMeshProUGUI dmgNum;
    Canvas uiCanvas;
    Vector3 screenPos;
    
    void Awake()
    {
        uiCanvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        

    }
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemies"))
        {
            IDamageable target = hitInfo.GetComponent<IDamageable>();
            
            if (target != null)
            {
                if(DmgPotion.dmgUp == true) target.TakeDamage((int)(dmg * DmgPotion.DmgAmount));
                else target.TakeDamage(dmg);
            }
            ShowImpactEffect();

            int finalDmg;
            if (DmgPotion.dmgUp) finalDmg = (int)(dmg * DmgPotion.DmgAmount);
            else finalDmg = dmg;
            ShowDmgText(finalDmg);

        }
        else if (hitInfo.CompareTag("Wall"))
        {
            ShowImpactEffect();
        }
        
        Destroy(gameObject);
    }

    void ShowImpactEffect()
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        
    }

    void ShowDmgText(int dmgValue)
    {
        // Convert world position to canvas position
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiCanvas.transform as RectTransform,
            Camera.main.WorldToScreenPoint(transform.position - new Vector3(-0.5f, 0,0)),
            uiCanvas.worldCamera,
            out canvasPos
        );

        TextMeshProUGUI dmgTextInstance = Instantiate(dmgNum, uiCanvas.transform);
        dmgTextInstance.text = dmgValue.ToString();
        dmgTextInstance.color = Random.ColorHSV(0f, 0.1f, 0.5f, 1f, 0.8f, 1f);

        // Set anchored position
        RectTransform rectTransform = dmgTextInstance.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = canvasPos;
        StartCoroutine(BounceText(rectTransform));
    }

    IEnumerator BounceText(RectTransform rectTransform)
    {
        float duration = 0.3f;
        float time = 0f;
        Vector3 startScale = Vector3.one;
        Vector3 peakScale = Vector3.one * 1.5f;

        while (time < duration)
        {
            float t = time / duration;
            // Ease out and in
            float scaleFactor = Mathf.Sin(t * Mathf.PI);
            rectTransform.localScale = Vector3.Lerp(startScale, peakScale, scaleFactor);
            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = startScale;
    }

}

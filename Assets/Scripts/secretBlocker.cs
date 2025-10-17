
using System.Xml.Serialization;
using UnityEngine;

public class secretBlocker : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer sr;
   

    private bool fading = false;
    private float fadeSpeed = 2f;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            Color startColor = sr.color;
            startColor.a = Mathf.MoveTowards(startColor.a, 0f, fadeSpeed * Time.deltaTime);
            sr.color = startColor;
            if (startColor.a <= 0f) fading = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            fading = true;
        
        }
    }
}

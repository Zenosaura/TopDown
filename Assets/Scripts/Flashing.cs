using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Flashing : MonoBehaviour
{
    TextMeshProUGUI totemText;
    // Start is called before the first frame update
    void Start()
    {
        totemText = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(TextFlash());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TextFlash()
    {
        totemText.overrideColorTags = true;

        while (true)
        {
            totemText.color = Color.red;
            yield return new WaitForSeconds(0.5f); // flash duration

            totemText.color = Color.white;
            yield return new WaitForSeconds(0.5f); // flash duration
        }
        
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class PotionPopUp : MonoBehaviour
{
    public TextMeshProUGUI potionText;

    bool dmgCounting = false;
    bool atkCounting = false;
    bool speedCounting = false;

    float dmgTime = 0f;
    float atkTime = 0f;
    float speedTime = 0f;

    void Update()
    {
        string display = "";

        if (DmgPotion.dmgUp && !dmgCounting)
        {
            dmgCounting = true;
            dmgTime = DmgPotion.dmgTime;
            StartCoroutine(DmgCountdown());
        }

        if (ATKPotion.dmgUp && !atkCounting)
        {
            atkCounting = true;
            atkTime = ATKPotion.dmgTime;
            StartCoroutine(AtkCountdown());
        }

        if (SpeedPotion.speedUp && !speedCounting)
        {
            speedCounting = true;
            speedTime = SpeedPotion.speedTime;
            StartCoroutine(SpeedCountdown());
        }

        if (dmgCounting) display += "DMG Boost: " + dmgTime.ToString("F1") + "s\n";
        if (atkCounting) display += "ATK Speed: " + atkTime.ToString("F1") + "s\n";
        if (speedCounting) display += "Speed Boost: " + speedTime.ToString("F1") + "s\n";

        potionText.text = display.TrimEnd('\n');
    }

    IEnumerator DmgCountdown()
    {
        dmgCounting = true;
        while (dmgTime > 0)
        {
            dmgTime -= Time.deltaTime;
            yield return null;
        }
        dmgCounting = false;
        dmgTime = 0f;
    }

    IEnumerator AtkCountdown()
    {
        atkCounting = true;
        while (atkTime > 0)
        {
            atkTime -= Time.deltaTime;
            yield return null;
        }
        atkCounting = false;
        atkTime = 0f;
    }

    IEnumerator SpeedCountdown()
    {
        speedCounting = true;
        while (speedTime > 0)
        {
            speedTime -= Time.deltaTime;
            yield return null;
        }
        speedCounting = false;
        speedTime = 0f;
    }
}

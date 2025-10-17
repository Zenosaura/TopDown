using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shoot;
    public AudioClip pickupGun;
    public AudioClip hurt;
    public AudioClip reload;
    public AudioClip slash;
    public AudioClip heal;
    public AudioClip speed;
    public AudioClip pickupSword;

    public void pickupSwordSound()
    {
        audioSource.PlayOneShot(pickupSword);
    }
    public void HealSound()
    {
        audioSource.PlayOneShot(heal);
    }
    public void SpeedSound()
    {
        audioSource.PlayOneShot(speed);
    }
    public void shootSound()
    {
        audioSource.PlayOneShot(shoot);
    }

    public void slashSound()
    {
        audioSource.PlayOneShot(slash);
    }

    public void pickupGunSound()
    {
        audioSource.PlayOneShot(pickupGun);
    }

    public void HurtSound()
    {
        audioSource.PlayOneShot(hurt);
    }
    public void ReloadSound()
    {
        audioSource.PlayOneShot(reload);
    }
}

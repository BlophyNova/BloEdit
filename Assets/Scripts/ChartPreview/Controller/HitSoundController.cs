using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundController : MonoBehaviour
{
    public AudioSource hitSound;
    public HitSoundController SetClip(AudioClip audioClip)
    {
        hitSound.clip = audioClip;
        return this;
    }
    public HitSoundController Play()
    {
        hitSound.Play();
        return this;
    }
}

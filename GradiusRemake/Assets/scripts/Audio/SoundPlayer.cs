using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioMaster master;
    // Start is called before the first frame update
    void Start()
    {
        master.playSoundCommand += PlaySound;
    }

   public void PlaySound(AudioClip clip)
   {
        source.clip = clip;
        source.Play();
   }
}

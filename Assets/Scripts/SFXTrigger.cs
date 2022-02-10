using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTrigger : MonoBehaviour
{
    // Very easy to use palce the scrip on a enemiy that will hurt the mouse f.x. Mousetrap or Rat and when they collide it will play any sound.
    public AudioSource playSound;

    void OnTriggerEnter(Collider other)
    {
        playSound.Play();
    }
}

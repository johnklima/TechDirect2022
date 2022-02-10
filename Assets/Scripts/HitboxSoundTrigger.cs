using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSoundTrigger : MonoBehaviour
{
    
    public AudioSource playSound;
    bool isPlayed = false; //bool = true/false statement means that it has not been played

    void OnTriggerEnter(Collider other) //entering hitbox
    {
        if(!isPlayed){ //checks if isPlayed is false, that means that it has not played
            playSound.PlayOneShot(playSound.clip,1.0f); //plays audio once
        }
            isPlayed = true; //should never be able to play again since isPlayed is changed to true without a way back to false
    
    }



 
}

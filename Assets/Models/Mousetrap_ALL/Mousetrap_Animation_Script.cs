using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetrap_Animation_Script : MonoBehaviour
{
   
    public bool triggerIsOn;
    public AudioSource audioSource;

    void Start ()
    {
        gameObject.GetComponent<AudioSource>().Play();
        

    }

    void Update ()
    {
        if (triggerIsOn)
        {
            gameObject.GetComponent<Animation>().Play();
        }
       



    }

    void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Player")
        {
            
            audioSource.enabled = true;
            triggerIsOn = true;
        }
        
    }
}
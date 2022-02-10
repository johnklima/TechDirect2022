using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public AudioSource menuAudio;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    public float volume = 0.5f;
   
    public void OnMouseHover()
    {
        menuAudio.PlayOneShot(hoverFx, volume);
    }
    public void OnMouseClick()
    {
        menuAudio.PlayOneShot(clickFx, volume);
    }
}

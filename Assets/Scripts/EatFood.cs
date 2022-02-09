using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : MonoBehaviour
{

    public AudioClip eatSound;

    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(eatSound, transform.position);
        Destroy(gameObject);
    }
}

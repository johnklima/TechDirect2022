using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextLevel : MonoBehaviour
{
    public bool isTrigger;

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            isTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            isTrigger = false;
        }
    }
}
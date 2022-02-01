using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    Gravity grav;
    // Start is called before the first frame update
    void Start()
    {
        Transform par = transform.parent;
        grav = par.GetComponent<Gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter " + other.name);

        if(other.tag == "Obstacle")
        {
            Vector3 dir = transform.parent.position - grav.previousPosition;
            transform.parent.position = grav.previousPosition - dir * 3.0f;

            grav.isColliding = true;

        }




    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            grav.isColliding = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Stay " + other.name);
    }
}

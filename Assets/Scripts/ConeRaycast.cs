using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (3) to get a bit mask
        int layerMask = 1 << 3;

        // This would cast rays only against colliders in layer 3.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        int count = transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Transform vertOff;
            vertOff = transform.GetChild(i);
            
            Transform vert;
            vert = vertOff.GetChild(0);
            //Debug.Log(vert.position.ToString());

            Vector3 dir = (vert.position - transform.position );
            dir.Normalize();

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, dir, out hit, 100.0f, layerMask))
            {
                Debug.Log("Did Hit " + hit.transform.name);
            }
            else
            {
               Debug.Log("Did not Hit");
            }

        }


        
    }
}

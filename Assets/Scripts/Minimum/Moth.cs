using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : MonoBehaviour
{

    public float distance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localPosition;
        if (pos.x < 0)
        {
            Debug.Log("MOTH IS DEAD");
            return;

        }

        //transform.Translate( -Vector3.right * distance * Time.deltaTime, Space.Self);
        
        pos.x -= distance * Time.deltaTime;
        transform.localPosition = pos;

    

    }
}

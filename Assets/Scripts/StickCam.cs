using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickCam : MonoBehaviour
{
    public Transform target;
    public float height = 3.0f;
    public float distance = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
  

        transform.position = target.position - target.forward * distance + target.up * height;
        transform.LookAt(target);

        
        
    }
}

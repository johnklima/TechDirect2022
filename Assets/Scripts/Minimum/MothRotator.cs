using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothRotator : MonoBehaviour
{

    public float speed = 100.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);

    }
}

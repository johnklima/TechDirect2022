using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    
    private LanderScript lander;

    public float turnspeed = 100.0f;
    public float walkspeed = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<LanderScript>();
    }

    // Update is called once per frame
    void Update()
    {

        float turn = Input.GetAxis("Horizontal");
        float walk = Input.GetAxis("Vertical");


        Debug.Log("turn = " + turn + " walk = " + walk);
        
        transform.Rotate(transform.up, turn * turnspeed * Time.deltaTime);

        Vector3 fwd = transform.forward;
        Vector3 newpos = transform.position + fwd * walk * walkspeed * Time.deltaTime;

        transform.position = newpos;




    }
}

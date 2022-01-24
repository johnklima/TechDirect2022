using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 velocity = new Vector3 (0,0,0);
    public Vector3 acceleration = new Vector3(0, 0, 0);
    public Vector3 finalForce = new Vector3(0, 0, 0);
    public Vector3 thrust = new Vector3(0, 0, 0);

    public float mass = 1;
    public float energy = 10.0f;
    public float crashMagnitude = 5.0f;

    //gravity in meters per second per second
    public float GRAVITY_CONSTANT = -9.8f;

    public bool isOnGround = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        handleInput();
        handleMovement();
        handleLanding();
        handleGrounded();

    }
    void handleInput()
    {
        

        if (Input.GetKey(KeyCode.Space)  && energy > 0)
        {
            thrust.y = 20.0f; //how strong is my engine?
            energy -= Time.deltaTime;
            isOnGround = false;
        }
    }
    void handleMovement()
    {
        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT * mass, 0);
        finalForce += thrust;

        acceleration = finalForce / mass;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        //reset thrust
        thrust = Vector3.zero;
    }
    void handleGrounded()
    {
        if (thrust.magnitude > 1.0f)
            isOnGround = false;

        if (isOnGround)
        {
            velocity.y = 0;           
        }
            
    }
    void handleLanding()
    {
        if (isOnGround)
            return;

        //did we hit the surface
        if(transform.position.y < 0)
        {
            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;
            if (velocity.magnitude > crashMagnitude)
                Debug.Log("SPLAT! " + velocity.magnitude.ToString() );
            else if( velocity.magnitude <= crashMagnitude )
                Debug.Log("WOOT! " + velocity.magnitude.ToString());

            isOnGround = true;

            velocity.y = 0;
        }

    }
}

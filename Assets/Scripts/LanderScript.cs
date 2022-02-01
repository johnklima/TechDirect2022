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
    public Vector3 jumpForce = new Vector3(0, 0, 0);

    public float mass = 1;
    public float energy = 10.0f;
    public float crashMagnitude = 5.0f;

    //gravity in meters per second per second
    public float GRAVITY_CONSTANT = -9.8f;

    public bool isOnGround = false;
    public bool isJumping = false;
    public bool isResting = false;
    public Vector3 groundPoint;

    public Quaternion restQ = new Quaternion();
    public Transform jumpTarget;
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
        handleResting();


        

    }
    void handleResting()
    {

        if (!isJumping && isResting)
        {
            Quaternion q = transform.rotation;
            transform.rotation = Quaternion.Slerp(q, restQ, Time.deltaTime * 5);
            if (Quaternion.Angle(q,restQ) < 1.0f)
            {
                isResting = false;
                transform.rotation = restQ;

            }
        }
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

        if (isOnGround)
            return;
        
        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT * mass, 0);
        finalForce += thrust;

        acceleration = finalForce / mass;

        velocity += acceleration * Time.deltaTime;

        velocity += jumpForce ;

        //Debug.Log("VELOCITY " + velocity.ToString());

        transform.position += velocity * Time.deltaTime;

        //point in the direction of the movement
        //Quaternion q = transform.rotation;
        //transform.LookAt(transform.position + velocity);
        //Quaternion q2 = transform.rotation;
        
        //interpolate
        //transform.rotation = Quaternion.Slerp(q, q2, Time.deltaTime * 5);

        //reset thrust
        thrust = Vector3.zero;
        jumpForce = Vector3.zero;
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
        float dist = Vector3.Distance(transform.position, groundPoint);

        if (dist < 0.95f) 
        {

            transform.position = groundPoint;
            if (velocity.magnitude > crashMagnitude)
            { } //   Debug.Log("SPLAT! " + velocity.magnitude.ToString() );
            else if (velocity.magnitude <= crashMagnitude)
            { } //    Debug.Log("WOOT! " + velocity.magnitude.ToString());

            isOnGround = true;
            isJumping = false;
            isResting = true;

            velocity = Vector3.zero;

            //get final rest look
            restQ = transform.rotation;
            Vector3 eul = restQ.eulerAngles;
            eul.x = 0;
            restQ = Quaternion.Euler(eul);



        }

    }
    public void Jump(Vector3 force)
    {
        isOnGround = false;
        isJumping = true;
        //velocity = force;// Vector3.zero;
        jumpForce = force;
        //jumpForce += transform.forward * 2.0f + Vector3.up * 2.0f;
        Debug.Log("JUMP " + force.ToString());

    }
}

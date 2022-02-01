using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 acceleration = new Vector3(0, 0, 0);
    public Vector3 finalForce = new Vector3(0, 0, 0);
    public Vector3 jumpForce = new Vector3(0, 0, 0);
   
    public Vector3 previousPosition = new Vector3(0, 0, 0);

    public float mass = 1.0f;
    public float GRAVITY_CONSTANT = -10.0f;
    public float JUMP_CONSTANT = 20.0f;
    public float JUMP_FORWARD = 5.0f;
    public bool isOnGround = false;
    public bool isJumping = false;
    public bool isColliding = false;

    public float turnSpeed = 100.0f;
    public float walkSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        previousPosition = transform.position;

        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            jumpForce = new Vector3(0, JUMP_CONSTANT, 0) + transform.forward * JUMP_FORWARD;
            isJumping = true;
        }


        handleGravity();
        handleGroundMovement();
    }
    void handleGroundMovement()
    {

        if(isOnGround && !isJumping && !isColliding)
        {
            float fwd = Input.GetAxis("Vertical");
            float turn = Input.GetAxis("Horizontal");

            turn *= turnSpeed * Time.deltaTime;
            transform.Rotate(0, turn, 0);

            Vector3 pos = transform.position;
            Vector3 forward = transform.forward;

            forward *= fwd * Time.deltaTime * walkSpeed;
            pos += forward;

            transform.position = pos;

        }

    }
    void handleGravity() 
    {

        //reset final force to the initial force of gravity
        finalForce.Set(0, GRAVITY_CONSTANT * mass, 0);
       
        acceleration = finalForce / mass;

        velocity += acceleration * Time.deltaTime;

        //Add instantaneous force aka explosion
        velocity += jumpForce;
        
        //Debug.Log("VELOCITY " + velocity.ToString());

        transform.position += velocity * Time.deltaTime;

        if(transform.position.y < 0.1f)
        {
            transform.position = previousPosition;
            isOnGround = true;
            velocity.y = 0;
            if(isJumping)
            {
                velocity = Vector3.zero;
                isJumping = false;
            }
        }
        else
        {
            isOnGround = false;
        }

        //reset instant force
        jumpForce = Vector3.zero;
  
        

    }
    
}

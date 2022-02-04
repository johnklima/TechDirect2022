using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityMouse : MonoBehaviour
{
    private float verticalVelocity = -3f;
    private float gravity = -7.0f;
    private float jumpSpeed = -0.5f;
    public bool isOnGround = false;
    public float distToGround = 0.01f;
    public float distToWall = 0.01f;
    public Vector3 velocity = new Vector3 (0,0,0);
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedCheck();
    }
    
    void groundedCheck()
    {
        //uses raycast to sense below the character. If anything gets 0.1f away from the character, it is sensed as a collision, and the falling velocity stops. 
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity *= jumpSpeed;
                transform.Translate(new Vector3(0, verticalVelocity * jumpSpeed , 0));
            }
        }
        //if the raycast does not sense anything below, it will continue falling downwards. 
       else
       {
           verticalVelocity += gravity * Time.deltaTime;
           transform.Translate(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
       }
            
    }

    //checks for layers with the name "Ground". If that is the case the boolean gets activated. If not, it is ignored.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        { isOnGround = true; }
        else 
        {isOnGround = false;}
    }
}

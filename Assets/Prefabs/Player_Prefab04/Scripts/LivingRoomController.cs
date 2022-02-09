using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomController : MonoBehaviour
{
    Transform character;
    public float stoppingSpeed = 0.5f;
    public float walkSpeed;
    public float strafeSpeed = 0.7f;
    public float friction;
    public float distToWall = 0.01f;
    public bool crashed = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        controllerMoving();
        collisionCheck();
        if(crashed == true)
        {
            walkSpeed = -3.0f;
        }
        else
        {
            walkSpeed = 1.5f;
            new Vector3(walkSpeed * Time.deltaTime, 0 , walkSpeed * Time.deltaTime);
        }
             
    }

    void collisionCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.one, distToWall + 0.3f))
        {
            crashed = true;
        }
       else
       {
           crashed = false;
       }
            
    }

    void controllerMoving()
    {
        float fwd = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        //applying strafe into controller. The player is moving (translated on the X-axis)
        strafe *= strafeSpeed * Time.deltaTime;
        transform.Translate(strafe, 0, 0);

        Vector3 pos = transform.position;
        Vector3 forward = transform.forward;

        forward *= fwd * Time.deltaTime * walkSpeed;
        pos += forward;
        transform.position = pos;
    }


}

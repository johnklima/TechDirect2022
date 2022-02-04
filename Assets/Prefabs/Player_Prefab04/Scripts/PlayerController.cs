using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    public float strafeSpeed = 5.0f;
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
    void Update()
    {
        controllerMoving();
        if(!crashed == true)
        {
            collisionCheck();
        }
        else
        {
    
        }
        
             
    }

    void collisionCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, distToWall + 0.3f))
        {
            crashed = true;
            Debug.Log("Crashed");
        }
       else
       {
           crashed = false;
           Debug.Log("NotCrashed");
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

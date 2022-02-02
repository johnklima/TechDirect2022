using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    
    private LanderScript lander;

    public float turnspeed = 100.0f;
    public float walkspeed = 10.0f;

    private Vector3 posCur;
    private Quaternion rotCur;

    // Start is called before the first frame update
    void Start()
    {
       lander = transform.GetComponent<LanderScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (lander.isJumping)
            return;

        float turn = Input.GetAxis("Horizontal");
        float walk = Input.GetAxis("Vertical");


        //Debug.Log("turn = " + turn + " walk = " + walk);

        transform.Rotate(transform.up, turn * turnspeed * Time.deltaTime);

        Vector3 fwd = transform.forward;
        Vector3 newpos = transform.position + fwd * walk * walkspeed * Time.deltaTime;

        transform.position = newpos;

        int layerMask = 1 << 3;

        RaycastHit hit;
        Ray ray = new Ray(transform.position + transform.up, -transform.up);
        if (Physics.Raycast(ray, out hit, 10000.0f, layerMask))
        {

            Debug.Log("GROUND POINT " + hit.collider.name);
            if (hit.distance > 2.0f)
            {
                lander.jumpTarget.position = hit.point;
                lander.isJumping = true;
                lander.isOnGround = false;
                lander.groundPoint = hit.point;
            }
            else
            { 
                rotCur = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
                posCur = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, posCur, Time.deltaTime * 5);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotCur, Time.deltaTime * 5);
            }
        }
    }
}

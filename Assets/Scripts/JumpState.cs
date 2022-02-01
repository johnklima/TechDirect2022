using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MonoBehaviour
{
    float GRAVITY = 9.8f; //hmmm as a positive force
    
    public Transform jumpTarget;
    private LanderScript landerScript;

    // Start is called before the first frame update
    void Start()
    {
        landerScript = GetComponent<LanderScript>();
        GRAVITY = Mathf.Abs(landerScript.GRAVITY_CONSTANT);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
           
            int layerMask = 1 << 3; 

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                Debug.Log(hit.collider.name);
                jumpTarget.position = hit.point;

                landerScript.groundPoint = hit.point;

                //float mag = Vector3.Distance(jumpTarget.position, transform.position);
                
                //get the current look
                Quaternion q = transform.rotation; 
                
                //look at the target to get the correct force vector                
                transform.LookAt(jumpTarget.position);   // + Vector3.up * mag );

                //START JUMP
                Vector3 jumpForce = calculateIterativeTrajectory(
                                    transform.position, 
                                    jumpTarget.position, 
                                    45);  //use zero for angle to allow the calc to find it
                                         //alternately, specify an angle regardless.

                //restore previous look, so we can interpolate the look for smoothing
                transform.rotation = q;
                
                //jump
                landerScript.Jump(jumpForce);
            
            }
        }
        //SIMPLE JUMP HERE


    }

    Vector3 calculateIterativeTrajectory(Vector3 startPoint, Vector3 endPoint, float desiredAngle)
    {

        Vector3 t;
        Vector3 c;

        t = endPoint;
        c = startPoint;

        //get a flat distance
        t.y = c.y = 0;
        float flatdistance = Vector3.Distance(t, c);

        Vector3 P1, P2, p2;
        p2 = P2 = endPoint - startPoint;

        //get just the flat direction without y component
        p2.y = 0;
        p2.Normalize();
        P1 = p2;

        //unitize the vector
        P2.Normalize();

        //get angle in rads, this is a minimum launch angle (if down to up)
        float angle = Mathf.Acos(Vector3.Dot(P1, P2));

        //any angle less than 45 is 45 (optimal angle)
        //any angle greater is the angle, plus HALF the angle of the vector to y up.
        //with balistics the angle ALWAYS has to be greater than the direct angle from
        //point to point. so we add half of this angle to 90, splitting the difference
        
        if (angle < 0.785398163f)
            angle = 0.785398163f;
        else
            angle += Mathf.Acos(Vector3.Dot(P1, Vector3.up)) * 0.5f;


        //if it is too close to pure vertical make it less than pure vertical
        if (angle > Mathf.PI / 2 - 0.05f)
            angle = Mathf.PI / 2 - 0.05f;

        //if we are going from up to down, just use appx 60 degs
        if (startPoint.y > endPoint.y)
            angle = 0.985398163f;

        
        //if we are asked to use a specific angle, this is our angle..
        if (desiredAngle != 0)
        {
            angle = Mathf.Deg2Rad * desiredAngle;
        }


        float Y = endPoint.y - startPoint.y;

        //are we firing down a hill?
        /*if (endPoint.y < startPoint.y)
        {
            angle *= -0.5f;
        }
        */

        // perform the trajectory calculation to arrive at the gun powder charge aka 
        // target velocity for the distance we desire, based on launch angle
        float rng = 0;
        float Vo = 0;
        float trydistance = flatdistance;
        int iters = 0;

        //now iterate until we find the correct proposed distance to land on spot.
        //this method is based on the idea of calculating the time to peak, and then
        //peak to landing at the height differential. we can then extract an XY distance 
        //achieved regardless of height differential. by iterating with a binary heuristic
        //we increase or decrease the initial velocity until we acheive our XY distance

        float f = (Mathf.Sin(angle * 2.0f));
        if (f <= 0)
            f = 1.0f;      //just for safety (though this should never be)

        while (Mathf.Abs(rng - flatdistance) > 0.001f && iters < 64)
        {
            //make sure we dont squirt on a negative number. we can IGNORE that result
            if (trydistance > 0)
            {

                //create an initial force 
                float sqrtcheck = (trydistance * GRAVITY) / f;
                if (sqrtcheck > 0)
                    Vo = Mathf.Sqrt(sqrtcheck);
                else
                { }// Debug.Log("sqrtcheck < 0 initial force");

                //find the vector of it in our trajectory planar space
                float Vy = Vo * Mathf.Sin(angle);
                float Vx = Vo * Mathf.Cos(angle);

                //get a height and thus time to peak
                float H = -Y + (Vy * Vy) / (2 * GRAVITY);
                float upt = Vy / GRAVITY;                      // time to max height

                //if again we squirt on a neg, but it is because our angle and force
                //are too accute. note: we handle up and down trajectory differently
                if (2 * H / GRAVITY < 0)
                {
                    if (endPoint.y < startPoint.y)
                        rng = flatdistance;       //if going down we are done
                    else
                        rng = 0;       //if up we are not
                }
                else
                {
                    sqrtcheck = 2 * H / GRAVITY;
                    if (sqrtcheck > 0)
                    {
                        float dnt = Mathf.Sqrt(sqrtcheck);           // time from max height to impact
                        rng = Vx * (upt + dnt);
                    }
                    else
                    { }//    Debug.Log("sqrtcheck < 0 H / Gravity");
                }

                if (rng > flatdistance)
                    trydistance -= (rng - flatdistance) / 2;		//using a binary zero-in, it takes about 8 iterations to arrive at target
                else if (rng < flatdistance)
                    trydistance += (flatdistance - rng) / 2;


                //Debug.Log("ITERS = " + iters);
            }
            else
            {

                iters = 64;
            }
            iters++;
        }

        
        Vector3 angV = new Vector3(transform.forward.x, 0, transform.forward.z);
        angV.Normalize();
        Vector3 side = Vector3.Cross(angV, Vector3.up);
        side.Normalize();
        //we need to rotate that by our actual launch angle
        angV = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, side) * angV;
        

        return angV * Vo;   //multiply by calculated "powder charge"   




    }
}

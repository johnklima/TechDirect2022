using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABoid : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform flock;
    public Vector3 velocity;

    public float cohesionFactor = 5.0f;
    public float separationFactor = 3.0f;
    public float alignFactor = 2.0f;
    public float constrainFactor = 2.0f;

    public float collisionDistance = 6.0f;
    public float speed = 1.0f;

    public Vector3 destabVector;
    public float destabFactor = 0.5f;
    public float groundFactor = 10.0f;

    void Start()
    {
        flock = transform.parent;

        Vector3 pos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        Vector3 look = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        float speed = Random.Range(0f, 5f);


        transform.position = pos;
        transform.LookAt(look);
        velocity = (look - pos) * speed;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVelocity = new Vector3(0, 0, 0);
        // rule 1 all boids steer towards center of mass - cohesion
        newVelocity += cohesion() * cohesionFactor;

        // rule 2 all boids steer away from each other - avoidance        
        newVelocity += separation() * separationFactor;

        // rule 3 all boids match velocity - allignment
        newVelocity += align() * alignFactor;

        //keep them within some bounds
        newVelocity += constrain() * constrainFactor;

        //destabalize
        newVelocity += destabVector * destabFactor;

        //avoid obstacles
        newVelocity += avoidGround() * groundFactor; 


        Vector3 slerpVelo = Vector3.Slerp(velocity, newVelocity, Time.deltaTime);

        velocity = slerpVelo;

        transform.position += velocity * Time.deltaTime * speed;
        transform.LookAt(transform.position + velocity);



    }
    Vector3 avoidGround()
    {

        Vector3 steer = new Vector3(0, 0, 0);

        int layerMask = 1 << 3;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 10000.0f, layerMask))
        {

            
            if (hit.distance < 10.0f)
            {
                steer = -transform.forward;
            }
        }

        return steer;
    }
    Vector3 constrain()
    {
        Vector3 steer = new Vector3(0, 0, 0);

        steer += (flock.position - transform.position);

        steer.Normalize();
        return steer;
    }


    Vector3 align()
    {
        Vector3 steer = new Vector3(0, 0, 0);
        int sibs = 0;

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += boid.GetComponent<ABoid>().velocity;
                sibs++;
            }

        }
        steer /= sibs;

        steer.Normalize();

        return steer;
    }

    Vector3 destabalize()
    {
        Vector3 steer = new Vector3(0, 0, 0);
        int sibs = 0;

        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += boid.GetComponent<ABoid>().destabVector;
                sibs++;
            }

        }
        steer /= sibs;

        steer.Normalize();

        return steer;
    }

    Vector3 cohesion()
    {
        Vector3 steer = new Vector3(0, 0, 0);

        int sibs = 0;           //count the boids, it might change

        //add up all positions
        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                steer += (Vector3)boid.transform.position;
                sibs++;
            }

        }

        steer /= sibs; //center of mass is the average position of all        

        steer -= (Vector3)transform.position;

        steer.Normalize();


        return steer;
    }

    Vector3 separation()
    {
        Vector3 steer = new Vector3(0, 0, 0);
        int sibs = 0;


        foreach (Transform boid in flock)
        {
            if (boid != transform)
            {
                if ((transform.position - boid.transform.position).magnitude < collisionDistance)
                {
                    steer += (Vector3)(transform.position - boid.transform.position);
                    sibs++;
                }

            }

        }
        steer /= sibs;
        steer.Normalize();        //unit, just direction
        return steer;

    }

}

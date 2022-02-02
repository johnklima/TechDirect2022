using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public float timer = -1;
    public float destab = 2.0f;
    public float interval = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timer > interval)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                ABoid boid = transform.GetChild(i).GetComponent<ABoid>();
                boid.destabVector += new Vector3(Random.Range(-destab, destab), Random.Range(-destab, destab), Random.Range(-destab, destab)).normalized;

            }
            interval = Random.Range(2, 5);
            timer = Time.time;
        }
    }
}

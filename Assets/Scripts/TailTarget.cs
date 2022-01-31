using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTarget : MonoBehaviour
{

    public float freq = 10;
    public float amp = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.localPosition;
        pos.x = Mathf.Sin(Time.time * freq) * amp;
        transform.localPosition = pos;
    }
}

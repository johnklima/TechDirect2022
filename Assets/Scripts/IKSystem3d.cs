using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem3d : MonoBehaviour
{
    public Segment3d[] segments;
    public int segcount = 0;
    public Transform target = null;

    public bool isReaching = false;
    public bool isDragging = false;

    private Segment3d lastSegment = null;
    private Segment3d firstSegment = null;

    
    // Use this for initialization
    void Awake()
    {

        //lets buffer our segements in an array
        segcount = transform.childCount;
        segments = new Segment3d[segcount];
        int i = 0;
        foreach (Transform child in transform)
        {
            segments[i] = child.GetComponent<Segment3d>();
            i++;
        }


        firstSegment = segments[0];
        lastSegment = segments[segcount - 1];
    }

    // Update is called once per frame
    void Update()
    {

      
        if (isDragging)
        {
            
            lastSegment.drag(target.position);
            
        }        
        else if (isReaching)
        {
            //call reach on the last
            lastSegment.reach(target.position);

            //and forward update on the first

            
            //we needed to maintain that first segment original position
            //which is the position of the IK system itself
            //COMMENT NEXT LINE AND IK ROOT WILL FOLLLOW TARGET:
            firstSegment.transform.position = transform.position;
            
            firstSegment.updateSegmentAndChildren();

        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{

   public Transform cameraOrbit;
   public Transform target;


   
   void Start()
   {
        cameraOrbit.position = target.position;
   }

   void Update()
   {
       transform.rotation = Quaternion.Euler(transform.position.x, transform.position.y, transform.position.z);
       transform.LookAt(target.position);
   }


}

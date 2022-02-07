using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector3 currentMouseLook;
    Vector3 appliedMouseDelta;
    Vector3 movementDirection;
    public float sensitivity = 1;
    public float smoothing = 2;
    public GameObject cameraOrbit;
    public Transform radius;
    public float rotationSpeed = 5.0f;
    public bool moving;
    

    private Vector3 initPosition;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calculates if the player is moving depending on horizontal and vertical axis from inputs.
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(targetVelocity.x != 0 || targetVelocity.z != 0)
        {
            moving = true;
            // Player has moved
            movingCamera();
        }
        else
        {
            //Player has not moved
            moving = false;
            idleCamera();
        }     
        
        scrollZoom(); 
    }

    public void movingCamera()
    {
        // // Get smooth mouse look. This rotates the camera around, depending on the input of either mouse axis. 
        Vector3 smoothMouseDelta = Vector3.Scale(new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector3.one * sensitivity * smoothing);
            
        // //player rotation to camera
        appliedMouseDelta = Vector3.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -360, 360);

        // Rotate camera and controller.
        transform.localRotation = Quaternion.AngleAxis(currentMouseLook.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
        radius.localRotation = Quaternion.identity;
    }

    public void idleCamera()
    {
        float h = rotationSpeed * Input.GetAxis("Mouse X");
        float v = rotationSpeed * Input.GetAxis("Mouse Y");
                
        //rotation of the orbit, using the euler angles of the GameObject that holds the camera.
        cameraOrbit.transform.eulerAngles = new Vector3(cameraOrbit.transform.eulerAngles.x, cameraOrbit.transform.eulerAngles.y + h, cameraOrbit.transform.eulerAngles.z + v); 
    }

    void scrollZoom()
    {
        //Scrolling mouse wheel allows camera orbit radius to increase/decrese
        float scrollFactor = Input.GetAxis("Mouse ScrollWheel");
        if (scrollFactor != 0)
        {
            cameraOrbit.transform.localScale = cameraOrbit.transform.localScale * (1f - scrollFactor);
        }
    }
}


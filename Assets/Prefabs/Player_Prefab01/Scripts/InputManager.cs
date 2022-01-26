using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    Transform character;
    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;
    public float sensitivity = 1;
    public float smoothing = 2;
    public GameObject cameraOrbit;
    public float rotationSpeed = 5.0f;

    // private Vector3 frozenDirection = Vector3.forward;
    // private bool isMouseAimFrozen = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // Get smooth mouse look.
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * sensitivity * smoothing);
        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / smoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

        // Rotate camera and controller.
        transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);

        
        float scrollFactor = Input.GetAxis("Mouse ScrollWheel");
        if (scrollFactor != 0)
        {
            cameraOrbit.transform.localScale = cameraOrbit.transform.localScale * (1f - scrollFactor);
        }
        
        //(WORK IN PROGRESS)

        // Freeze the mouse aim direction when the free look key is pressed.
            // if (Input.GetKeyDown(KeyCode.C))
            // {
            //     isMouseAimFrozen = true;
            //     frozenDirection = Vector3.forward;
            // }
            // else if  (Input.GetKeyUp(KeyCode.C))
            // {
            //     isMouseAimFrozen = false;
            //     transform.forward = frozenDirection;
            // }

    
    }


}


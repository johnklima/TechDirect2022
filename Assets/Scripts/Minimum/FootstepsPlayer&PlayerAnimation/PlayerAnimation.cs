using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    //private float vert;
    //private float vertHorizontal;
    //public bool isWalking;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()

    {
        //// Add to walk parameter if a direction is pressed
        //vert = Input.GetAxis("Vertical");
        //anim.SetFloat("Walk", vert);
        //vert = Input.GetAxis("Horizontal");
        //anim.SetFloat("WalkHorizontal", vert);


        //Check if player is pressing a movement key
        if ((Input.GetButton("Horizontal")) || (Input.GetButton("Vertical")))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // Set rotation of the mesh to direction pressed

        // Right
        if ((Input.GetButton("Horizontal")) && (Input.GetAxisRaw("Horizontal") > 0))

        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        // Left
        if ((Input.GetButton("Horizontal")) && (Input.GetAxisRaw("Horizontal") < 0))

        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }

        // Up
        if ((Input.GetButton("Vertical")) && (Input.GetAxisRaw("Vertical") > 0))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        // Up-left
        if (((Input.GetButton("Horizontal")) && (Input.GetAxisRaw("Horizontal") > 0)) && ((Input.GetButton("Vertical")) && (Input.GetAxisRaw("Vertical") > 0)))

        {
            transform.localRotation = Quaternion.Euler(0, 45, 0);
        }

        // Up-Right
        if (((Input.GetButton("Horizontal")) && (Input.GetAxisRaw("Horizontal") < 0)) && ((Input.GetButton("Vertical")) && (Input.GetAxisRaw("Vertical") > 0)))

        {
            transform.localRotation = Quaternion.Euler(0, -45, 0);
        }

        // Down
        if ((Input.GetButton("Vertical")) && (Input.GetAxisRaw("Vertical") < 0))
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        // Down-right
        if (((Input.GetButton("Horizontal")) && (Input.GetAxisRaw("Horizontal") > 0)) && ((Input.GetButton("Vertical")) && (Input.GetAxisRaw("Vertical") < 0)))

        {
            transform.localRotation = Quaternion.Euler(0, 135, 0);
        }

        // Down-left
        if (((Input.GetButton("Horizontal")) && (Input.GetAxisRaw("Horizontal") < 0)) && ((Input.GetButton("Vertical")) && (Input.GetAxisRaw("Vertical") < 0)))

        {
            transform.localRotation = Quaternion.Euler(0, -135, 0);
        }

    }
}
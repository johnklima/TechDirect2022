using UnityEngine;

public class FootstepsPlayer : MonoBehaviour
{
    public float speed = 5;
    public AudioSource source;
    public AudioClip footstepLeftWood;
    public AudioClip footstepRightWood;
    public AudioClip footstepLeftBox;
    public AudioClip footstepRightBox;
    public AudioClip footstepLeftBed;
    public AudioClip footstepRightBed;
    private float stepRate;
    private bool leftFoot;


    private void Start()
    {
        leftFoot = true;
    }

    void FixedUpdate()
    {

        {

            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
            {
                
                stepRate += 1;
            }

            if (stepRate >= speed)
            {
                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit terrain, 0.1f + 0.1f))
                {
                    if (leftFoot == true)
                    {
                        if (terrain.transform.CompareTag("Wood"))
                        {
                            //Debug.Log("wood hit");
                            source.clip = footstepLeftWood;

                        }

                        if (terrain.transform.CompareTag("Box"))
                        {
                            //Debug.Log("box hit");
                            source.clip = footstepLeftBox;

                        }

                        if (terrain.transform.CompareTag("Bed"))
                        {
                            //Debug.Log("bed hit");
                            source.clip = footstepLeftBed;

                        }

                        source.Play();
                        stepRate = 0;
                        leftFoot = false;

                    }

                    else
                    {

                        if (terrain.transform.CompareTag("Wood"))
                        {
                            //Debug.Log("wood hit");
                            source.clip = footstepRightWood;
                        }

                        if (terrain.transform.CompareTag("Box"))
                        {
                            //Debug.Log("box hit");
                            source.clip = footstepRightBox;
                        }

                        if (terrain.transform.CompareTag("Bed"))
                        {
                            //Debug.Log("bed hit");
                            source.clip = footstepRightBed;
                        }

                        source.Play();
                        stepRate = 0;
                        leftFoot = true;

                    }
                }
            }
        }
    }
}
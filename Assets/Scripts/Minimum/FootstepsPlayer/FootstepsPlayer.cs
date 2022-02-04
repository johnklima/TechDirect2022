using UnityEngine;

public class FootstepsPlayer : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;
    public AudioSource source;
    public AudioClip footstepLeft;
    public AudioClip footstepRight;
    private float stepRate;
    private bool leftFoot;

    private void Start()
    {
        leftFoot = true;
    }

    void FixedUpdate()
    {

        //if ((velocity.y != 0) || (velocity.x != 0))
        if ((Input.GetAxis("Horizontal")!=0) || (Input.GetAxis("Vertical") != 0))
        {
            stepRate += 1;
        }

        if (stepRate == 6)
        {

            if (leftFoot == true)
            {
                Debug.Log("Left");
                source.clip = footstepLeft;
                source.Play();
                stepRate = 0;
                leftFoot = false;
            }

            else
            {
                Debug.Log("right");
                source.clip = footstepRight;
                source.Play();
                stepRate = 0;
                leftFoot = true;
            }
        }
    }
}
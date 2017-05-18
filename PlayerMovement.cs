using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Componenets
    private Rigidbody rb;
    public Transform direction;

    //Control Variables
    private bool running = false;
    private bool reseting = false;
    //Jump Variables
    public float jumpForce = 3f;
    private bool jumpping = false;

    //Crouch Variables
    public Transform wrapperTransform;
    private bool crouching = false;

    //Speed Variables
    public float baseMovementSpeed = 20f;
    private float movementSpeed;
    public float sideMovementSpeed = 5f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = baseMovementSpeed;

        running = true;
    }

    bool isGrounded()
    {
        return Mathf.Abs(rb.velocity.y) <= 0.05 && transform.position.y <= 0.9f;
    }

    IEnumerator resetRotation()
    {
        yield return new WaitForSeconds(0.8f);

        movementSpeed = baseMovementSpeed;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero; stops rotating velocity

        transform.position = new Vector3(transform.position.x, ResetPosition.groundLevel, transform.position.z);

        reseting = false;
    }

    public void setRunning(bool running)
    {
        this.running = running;

        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (reseting || !running)
            return;

        //Add force to move player forward
        rb.AddForce(direction.forward * movementSpeed * Time.fixedDeltaTime * 100);

        //Remove force if moving too fast
        if (rb.velocity[2] > 20f)
        {
            //rb.AddForce(-direction.forward * movementSpeed * Time.fixedDeltaTime * 100);
        }

        //If Player is on their side or front reset their position
        if ( ( (Mathf.Abs(transform.eulerAngles.x) > 80 && Mathf.Abs(transform.eulerAngles.x) < 100) | 
               (Mathf.Abs(transform.eulerAngles.y) > 80 && Mathf.Abs(transform.eulerAngles.y) < 100) |
               (Mathf.Abs(transform.eulerAngles.x) > 250 && Mathf.Abs(transform.eulerAngles.x) < 280) |
               (Mathf.Abs(transform.eulerAngles.y) > 250 && Mathf.Abs(transform.eulerAngles.y) < 280) )
            && !reseting)
        {
            StartCoroutine("resetRotation");
            reseting = true;
        }

        //Key Inputs----------------------
        //Side movements
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-direction.right * sideMovementSpeed * 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(direction.right * sideMovementSpeed * 10);
        }
        //Jumpping
        if (Input.GetKey(KeyCode.Space) && isGrounded() && !crouching)
        {
            rb.AddForce(direction.up * jumpForce * 100);
        }
        //Crouch
        if (Input.GetKey(KeyCode.LeftControl) && isGrounded())
        {
            crouching = true;
            Vector3 scale = wrapperTransform.localScale;
            wrapperTransform.localScale = new Vector3(scale.x, 0.5f, scale.z);
        }
        else if (!Input.GetKey(KeyCode.LeftControl) && crouching)
        {
            crouching = false;
            Vector3 scale = wrapperTransform.localScale;
            wrapperTransform.localScale = new Vector3(scale.x, 1.1f, scale.z);
        }

        movementSpeed += 0.2f;
    }
}

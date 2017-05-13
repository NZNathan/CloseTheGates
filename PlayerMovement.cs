using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Componenets
    private Rigidbody rb;
    public Transform direction;

    //Jump Variables
    public float jumpForce = 3f;
    private bool jumpping = false;

    //Speed Variables
    public float baseMovementSpeed = 20f;
    private float movementSpeed;
    public float sideMovementSpeed = 5f;
    private bool reseting = false;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = baseMovementSpeed;

    }

    bool isGrounded()
    {
        return Mathf.Abs(rb.velocity.y) <= 0.05 && transform.position.y <= 0;
    }

    IEnumerator resetRotation()
    {
        yield return new WaitForSeconds(0.8f);

        movementSpeed = baseMovementSpeed;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;
        reseting = false;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (reseting)
            return;

        Debug.Log(transform.eulerAngles.x);

        rb.AddForce(direction.forward * movementSpeed * Time.fixedDeltaTime * 100);

        if (rb.velocity[2] > 20f)
        {
            //rb.AddForce(-direction.forward * movementSpeed * Time.fixedDeltaTime * 100);
        }

        //If Player is on their side or front
        if ( ( (Mathf.Abs(transform.eulerAngles.x) > 80 && Mathf.Abs(transform.eulerAngles.x) < 100) | 
               (Mathf.Abs(transform.eulerAngles.y) > 80 && Mathf.Abs(transform.eulerAngles.y) < 100) |
               (Mathf.Abs(transform.eulerAngles.x) > 250 && Mathf.Abs(transform.eulerAngles.x) < 280) |
               (Mathf.Abs(transform.eulerAngles.y) > 250 && Mathf.Abs(transform.eulerAngles.y) < 280) )
            && !reseting)
        {
            StartCoroutine("resetRotation");
            reseting = true;
        }

        //Key Inputs
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-direction.right * sideMovementSpeed * 10);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(direction.right * sideMovementSpeed * 10);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(direction.up * jumpForce * 100);
        }

        movementSpeed += 0.2f;
    }
}

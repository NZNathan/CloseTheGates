using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Componenets
    private Rigidbody rb;
    public Transform direction;

    //Movement Variables
    private bool movingLeftBtn = false;
    private bool movingRightBtn = false;
    private bool jumpBtn = false;
    private bool crouchBtn = false;

    //Control Variables
    public static float groundLevel;
    private bool running = false;
    private bool reseting = false;
    private float yAngle = 0;
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

        groundLevel = transform.position.y;

    }

    bool isGrounded()
    {
        return Mathf.Abs(rb.velocity.y) <= 0.05 && transform.position.y <= 0.9f;
    }

    IEnumerator resetRotation()
    {
        yield return new WaitForSeconds(0.8f);

        transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);

        resetVelocity();
    }

    //Reset velocity and movement speed
    public void resetVelocity()
    {
        movementSpeed = baseMovementSpeed;

        transform.localEulerAngles = new Vector3(0, yAngle, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        reseting = false;
    }

    public void setRunning(bool running)
    {
        this.running = running;

        rb.velocity = Vector3.zero;
    }

    public bool isRunning()
    {
        return running;
    }

    public void setYAngle(float yAngle)
    {
        this.yAngle = yAngle;
    }

    void Update()
    {
        if (!running)
            return;

        if (Input.GetKeyDown(KeyCode.A))// || Input.GetKey(KeyCode.A)) ? So keys work after menu
        {
            movingLeftBtn = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            movingRightBtn = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBtn = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouchBtn = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            movingLeftBtn = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            movingRightBtn = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpBtn = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouchBtn = false;
        }

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
        if (movingLeftBtn)
        {
            Debug.Log("Hit A key at " + Timer.instance.getTime());
            rb.AddForce(-direction.right * sideMovementSpeed * 10);
        }
        if (movingRightBtn)
        {
            rb.AddForce(direction.right * sideMovementSpeed * 10);
        }
        //Jumpping
        if (jumpBtn && isGrounded() && !crouching)
        {
            rb.AddForce(direction.up * jumpForce * 100);
        }
        //Crouch
        if (crouchBtn && isGrounded())
        {
            crouching = true;
            Vector3 scale = wrapperTransform.localScale;
            wrapperTransform.localScale = new Vector3(scale.x, 0.5f, scale.z);
        }
        else if (!crouchBtn && crouching)
        {
            crouching = false;
            Vector3 scale = wrapperTransform.localScale;
            wrapperTransform.localScale = new Vector3(scale.x, 1.1f, scale.z);
        }

        movementSpeed += 0.2f;
    }
}

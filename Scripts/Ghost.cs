using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{

    //Componenets
    private Rigidbody rb;
    public Transform direction;

    //Time Variables
    private float fastestTime = float.MaxValue;

    //Ghost Movements
    public GhostMovement movement;
    private List<TimeStamp> movements;

    //Ghost Controls
    private bool movingLeft = false;
    private bool movingRight = false;
    private bool jumpping = false;
    private bool crouchPressed = false;

    //Control Variables
    public static float groundLevel;
    private bool running = false;
    private bool reseting = false;
    //Jump Variables
    public float jumpForce = 3f;

    //Crouch Variables
    public Transform wrapperTransform;
    private bool crouching = false;

    //Speed Variables
    public float baseMovementSpeed = 20f;
    private float movementSpeed;
    public float sideMovementSpeed = 5f;

    // Use this for initialization
    public void intialise()
    {
        rb = GetComponent<Rigidbody>();
        movementSpeed = baseMovementSpeed;

        groundLevel = transform.position.y;

        movements = movement.getMovements();
        running = false;

        resetVelocity();
    }

    bool isGrounded()
    {
        return Mathf.Abs(rb.velocity.y) <= 0.05 && transform.position.y <= 0.9f;
    }

    IEnumerator resetGhost()
    {
        yield return new WaitForSeconds(0.8f);

        transform.position = new Vector3(transform.position.x, groundLevel, transform.position.z);

        resetVelocity();
    }

    //Reset velocity and movement speed
    public void resetVelocity()
    {
        movementSpeed = baseMovementSpeed;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        reseting = false;
    }

    public bool isRunning()
    {
        return running;
    }

    public void print()
    {
        movement.print();
    }

    public void setRunning(bool running)
    {
        this.running = running;
    }

    void Update()
    {
        if (!running)
            return;

        TimeStamp nextAction;

        try
        {
            nextAction = movements[0];
            while(nextAction.getTime() <= Timer.instance.getTime())
            {
                Debug.Log(nextAction.getTime() + " vs actual time " + Timer.instance.getTime() + " Time diff: " + (Timer.instance.getTime() - nextAction.getTime()) + " " + nextAction.wasPressed());
                string action = nextAction.getAction();

                if (action == "left")
                {
                    movingLeft = nextAction.wasPressed();
                }
                if (action == "right")
                {
                    movingRight = nextAction.wasPressed();
                }
                if (action == "jump")
                {
                    jumpping = nextAction.wasPressed();
                }
                if (action == "crouch")
                {
                    crouchPressed = nextAction.wasPressed();
                }

                movements.Remove(nextAction);
                nextAction = movements[0];
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (reseting | !running)
            return;

        //Add force to move player forward
        rb.AddForce(direction.forward * movementSpeed * Time.fixedDeltaTime * 100);

        //If Ghost is on their side or front reset their position
        if (((Mathf.Abs(transform.eulerAngles.x) > 80 && Mathf.Abs(transform.eulerAngles.x) < 100) |
               (Mathf.Abs(transform.eulerAngles.y) > 80 && Mathf.Abs(transform.eulerAngles.y) < 100) |
               (Mathf.Abs(transform.eulerAngles.x) > 250 && Mathf.Abs(transform.eulerAngles.x) < 280) |
               (Mathf.Abs(transform.eulerAngles.y) > 250 && Mathf.Abs(transform.eulerAngles.y) < 280))
            && !reseting)
        {
            StartCoroutine("resetGhost");
            reseting = true;
        }

        //Key Inputs----------------------
        //Side movements
        if (movingLeft)
        {
            rb.AddForce(-direction.right * sideMovementSpeed * 10);
        }
        if (movingRight)
        {
            rb.AddForce(direction.right * sideMovementSpeed * 10);
        }
        //Jumpping
        if (jumpping && isGrounded() && !crouching)
        {
            rb.AddForce(direction.up * jumpForce * 100);
        }
        //Crouch
        if (crouchPressed && isGrounded())
        {
            crouching = true;
            Vector3 scale = wrapperTransform.localScale;
            wrapperTransform.localScale = new Vector3(scale.x, 0.5f, scale.z);
        }
        else if (!crouchPressed && crouching)
        {
            crouching = false;
            Vector3 scale = wrapperTransform.localScale;
            wrapperTransform.localScale = new Vector3(scale.x, 1.1f, scale.z);
        }

        movementSpeed += 0.2f;
    }
}

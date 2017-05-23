using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {

    //Movement Variables
    private float fastestTime;
    private List<TimeStamp> fastestMovements;
    private List<TimeStamp> movements;

    //Control Variables
    private PlayerMovement player;

    //Timer Offset Variables
    private float releaseTime = 0.025f;
    private float offsetTime = 0.01f;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        movements = new List<TimeStamp>();
    }

    //Returns movements and resets the list
    public List<TimeStamp> getMovements()
    {
        List<TimeStamp> moveset = new List<TimeStamp>();
        foreach (TimeStamp t in movements)
        {
            moveset.Add(t);
        }

        movements = new List<TimeStamp>();
        return moveset;
    }

    public void print()
    {
        foreach(TimeStamp t in movements)
        {
            Debug.Log("Time: " + t.getTime());
        }
    }
	
	// Get unfiltered button presses (doesn't check if movement is allowed or not)
	void Update ()
    {
        if (!player.isRunning())
            return;

        //Press Button
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Adding A press at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("left", Timer.instance.getTime() - offsetTime, true));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Adding D press at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("right", Timer.instance.getTime() - offsetTime, true));
        }
        //Jumpping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Adding Jump press at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("jump", Timer.instance.getTime() - offsetTime, true));
        }
        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("Adding Crouch press at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("crouch", Timer.instance.getTime() - offsetTime, true));
        }

        //Release Button 0.0165664 difference from releasing key to last call in player movement
        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("Adding A release at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("left", Timer.instance.getTime() - releaseTime, false));
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("Adding D release at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("right", Timer.instance.getTime() - releaseTime, false));
        }
        //Jumpping
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Adding Jump release at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("jump", Timer.instance.getTime() - releaseTime, false));
        }
        //Crouch
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Debug.Log("Adding Crouch release at time " + Timer.instance.getTime());
            movements.Add(new TimeStamp("crouch", Timer.instance.getTime() - releaseTime, false));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStamp {

    private string action;
    private float time;
    private bool pressed;

	// Use this for initialization
	public TimeStamp(string action, float time, bool pressed)
    {
        this.action = action;
        this.time = time;
        this.pressed = pressed;
    }

    public string getAction()
    {
        return action;
    }

    public float getTime()
    {
        return time;
    }

    public bool wasPressed()
    {
        return pressed;
    }
	
}

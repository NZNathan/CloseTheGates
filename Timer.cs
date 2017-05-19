using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static Timer instance;

    //Timer Variables
    private bool timerRunning = true;
    private Text timerText;
    private float time;

	// Use this for initialization
	void Start ()
    {
        timerText = GetComponent<Text>();
        instance = this;
    }
	
    public float getTime()
    {
        return time;
    }

    public void setTimer(bool timer)
    {
        timerRunning = timer;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!timerRunning)
            return;

        time += Time.deltaTime;

        var seconds = time;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        //update the label value
        timerText.text = string.Format("Time: \n{0:0}.{1:00}", Mathf.Floor(seconds), fraction);
    }
}

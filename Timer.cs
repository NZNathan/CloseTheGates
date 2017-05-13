using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    //Timer Variables
    private Text timerText;
    private float time;

	// Use this for initialization
	void Start ()
    {
        timerText = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        var seconds = time;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        //update the label value
        timerText.text = string.Format("Time: \n{0:0}.{1:00}", Mathf.Floor(seconds), fraction);
    }
}

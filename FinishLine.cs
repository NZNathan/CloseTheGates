using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Player has crossed finish line
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setRunning(false);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    public static float groundLevel;

    private GameObject player;  

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        groundLevel = player.transform.position.y;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            player.transform.position = new Vector3(3.85f, groundLevel, -15f);
    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R))
            player.transform.position = new Vector3(3.85f, groundLevel, -15f);
    }
}

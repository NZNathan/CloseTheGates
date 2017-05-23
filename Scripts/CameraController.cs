using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private GameObject ghost;
    private Vector3 offset = new Vector3(0,0,-1);

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        ghost = GameObject.Find("Ghost");
    }
	
	// Update is called once per frame
	void Update ()
    {
            transform.position = player.transform.position + offset;
    }
}

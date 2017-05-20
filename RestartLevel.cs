using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour {

    private PlayerMovement player;
    private Vector3 startPos;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        startPos = player.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.transform.position = startPos;
            player.StopAllCoroutines();
            player.resetVelocity();
            Timer.instance.resetTimer();
            Timer.instance.setTimerRunning(true);
        }
    }
}

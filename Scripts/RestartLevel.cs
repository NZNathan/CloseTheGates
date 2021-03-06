﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour {

    public HighScorePanel highScorePanel;

    private PlayerMovement player;
    private Ghost ghost;
    private Transform playerBaesDir;
    private Vector3 startPos;
    
    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerBaesDir = GameObject.Find("PlayerBaseDir").GetComponent<Transform>();
        ghost = GameObject.Find("Ghost").GetComponent<Ghost>();

        startPos = player.transform.position;

        ghost.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player has crossed finish line
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setRunning(false);
            levelFinished();
        }
        //Player has crossed finish line
        if (other.tag == "Ghost")
        {
            other.GetComponent<Ghost>().setRunning(false);
        }
    }

    public void restartLevel()
    {
        //Reset player 
        player.transform.position = startPos;
        player.setYAngle(0);
        player.StopAllCoroutines();
        player.resetVelocity();

        //Reset ghost
        ghost.gameObject.SetActive(true);
        ghost.transform.position = startPos;
        ghost.transform.eulerAngles = new Vector3(0, 0, 0);
        ghost.StopAllCoroutines();
        ghost.intialise();

        player.setRunning(true);
        ghost.setRunning(true);

        //Restart PlayerBaseDir
        playerBaesDir.eulerAngles = new Vector3(0,0,0);

        //Reset Timer
        Timer.instance.resetTimer();
        Timer.instance.setTimerRunning(true);

        //Close highscore board
        highScorePanel.gameObject.SetActive(false);
    }

    void levelFinished()
    {
        Timer.instance.setTimerRunning(false);
        highScorePanel.gameObject.SetActive(true);
        highScorePanel.StartCoroutine("setupHighScorePanel", Timer.instance.getTime());
        ghost.print();
    }

}

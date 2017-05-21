using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevel : MonoBehaviour {

    public HighScorePanel highScorePanel;

    private PlayerMovement player;
    private Vector3 startPos;
    
    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        startPos = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player has crossed finish line
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().setRunning(false);
            levelFinished();
        }
    }

    void restartLevel()
    {
        //Reset player 
        player.transform.position = startPos;
        player.StopAllCoroutines();
        player.resetVelocity();

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
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartLevel();
        }
    }
}
